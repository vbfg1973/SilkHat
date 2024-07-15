using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using QuikGraph;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.AST;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure;
using SilkHat.Domain.Graph.GraphEngine;
using SilkHat.Domain.Graph.GraphEngine.Abstract;
using SilkHat.Domain.Graph.GraphEngine.GraphAnalysers;
using SilkHat.Domain.Graph.GraphEngine.GraphAnalysers.Models;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Triples;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions
{
    public interface ISolutionCollection
    {
        bool IsLoading { get; }
        Task<SolutionModel> AddSolution(string solutionPath);
        Task<List<SolutionModel>> SolutionsInCollection();
        Task<List<ProjectModel>> ProjectsInSolution(SolutionModel solutionModel);
        Task<ProjectStructureModel> ProjectStructure(ProjectModel projectModel);
        Task<SyntaxStructure> SyntaxStructure(ProjectModel projectModel, string fullPath);
        Task<DocumentModel> GetDocument(ProjectModel projectModel, string fullPath);
        Task<List<TypeDefinition>> GetPathStructure(ProjectModel projectModel, string fullPath);
        Task<EnhancedDocumentModel> GetEnhancedDocument(ProjectModel projectModel, string fullPath);
    }

    public class SolutionCollection :
        IObservable<SolutionCollection.SolutionLoadedNotification>, ISolutionCollection
    {
        private readonly ILogger<SolutionCollection> _logger;
        private readonly ISolutionAnalyserFactory _solutionAnalyserFactory;
        private readonly ITripleGraph _tripleGraph;
        private readonly ITripleGraphAnalyserFactory _tripleGraphAnalyserFactory;

        private readonly ConcurrentDictionary<SolutionModel, ISolutionAnalyser> _solutionAnalysers = new();

        public SolutionCollection(ISolutionAnalyserFactory solutionAnalyserFactory, ITripleGraph tripleGraph, ITripleGraphAnalyserFactory tripleGraphAnalyserFactory,  ILoggerFactory loggerFactory)
        {
            _solutionAnalyserFactory = solutionAnalyserFactory;
            _tripleGraph = tripleGraph;
            _tripleGraphAnalyserFactory = tripleGraphAnalyserFactory;
            _logger = loggerFactory.CreateLogger<SolutionCollection>();
        }

        public bool IsLoading { get; private set; }

        public async Task<SolutionModel> AddSolution(string solutionPath)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                SolutionAnalyser solution = _solutionAnalyserFactory.Create(new SolutionAnalyserOptions(solutionPath));
                await solution.LoadSolution();
                await solution.BuildSolution();

                Parallel.ForEach(solution.Projects, projectModel =>
                {
                    solution.CodeAnalysis(projectModel).Wait();
                });

                await _tripleGraph.LoadTriples(solution.Triples);
                
                stopWatch.Stop();
                await Console.Error.WriteLineAsync($"Loaded {solution.Triples.Count} triples for {solution.Solution.Name} in {stopWatch.Elapsed}");
                
                _solutionAnalysers.TryAdd(solution.Solution, solution);

                SolutionLoadedNotify(solution.Solution);
                IsLoading = false;
                return solution.Solution;
            }

            _logger.LogWarning("Already loading a solution");
            return null!;
        }

        public async Task<List<SolutionModel>> SolutionsInCollection()
        {
            return _solutionAnalysers
                .Select(x => x.Value.Solution)
                .OrderBy(x => x.Name)
                .ToList();
        }

        public async Task<List<ProjectModel>> ProjectsInSolution(SolutionModel solutionModel)
        {
            TryGetSolutionAnalyser(solutionModel, out ISolutionAnalyser solutionAnalyser);
            return solutionAnalyser.Projects;
        }

        public async Task<ProjectStructureModel> ProjectStructure(ProjectModel projectModel)
        {
            TryGetSolutionAnalyser(projectModel.SolutionModel, out ISolutionAnalyser solutionAnalyser);
            return await solutionAnalyser.ProjectStructure(projectModel);
        }

        public async Task<SyntaxStructure> SyntaxStructure(ProjectModel projectModel, string fullPath)
        {
            TryGetSolutionAnalyser(projectModel.SolutionModel, out ISolutionAnalyser solutionAnalyser);
            return await solutionAnalyser.SyntaxStructure(projectModel, fullPath);
        }

        public async Task<DocumentModel> GetDocument(ProjectModel projectModel, string fullPath)
        {
            TryGetSolutionAnalyser(projectModel.SolutionModel, out ISolutionAnalyser solutionAnalyser);
            return await solutionAnalyser.DocumentModel(projectModel, fullPath);
        }

        public async Task<List<TypeDefinition>> GetPathStructure(ProjectModel projectModel, string fullPath)
        {
            var analyzer = _tripleGraphAnalyserFactory.CreateTripleGraphAnalyzer<TypeStructureInFileGraphAnalyzer>();
            return analyzer.TypeStructure(fullPath).ToList();
        }

        public async Task<EnhancedDocumentModel> GetEnhancedDocument(ProjectModel projectModel, string fullPath)
        {
            TryGetSolutionAnalyser(projectModel.SolutionModel, out ISolutionAnalyser solutionAnalyser);
            return await solutionAnalyser.EnhancedDocumentModel(projectModel, fullPath);
        }

        private bool TryGetSolutionAnalyser(SolutionModel solutionModel, out ISolutionAnalyser solutionAnalyser)
        {
            solutionAnalyser = null!;

            if (!_solutionAnalysers.TryGetValue(solutionModel, out ISolutionAnalyser? solutionAnalyserFromDict))
                return false;
            solutionAnalyser = solutionAnalyserFromDict;

            return true;
        }

        #region Solution Loaded Observable

        public record SolutionLoadedNotification(SolutionModel SolutionModel);

        private readonly List<IObserver<SolutionLoadedNotification>> _solutionLoadedObservers = new();

        private void SolutionLoadedNotify(SolutionModel solutionModel)
        {
            SolutionLoadedNotification notification = new(solutionModel);

            foreach (IObserver<SolutionLoadedNotification> observer in _solutionLoadedObservers)
            {
                observer.OnNext(notification);
            }
        }

        public IDisposable Subscribe(IObserver<SolutionLoadedNotification> observer)
        {
            _solutionLoadedObservers.Add(observer);

            return new SolutionLoadedUnsubscriber(_solutionLoadedObservers, observer);
        }

        private class SolutionLoadedUnsubscriber : IDisposable
        {
            private readonly IObserver<SolutionLoadedNotification> _observer;
            private readonly List<IObserver<SolutionLoadedNotification>> _observers;

            public SolutionLoadedUnsubscriber(List<IObserver<SolutionLoadedNotification>> observers,
                IObserver<SolutionLoadedNotification> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                _observers.Remove(_observer);
            }
        }

        #endregion
    }
}