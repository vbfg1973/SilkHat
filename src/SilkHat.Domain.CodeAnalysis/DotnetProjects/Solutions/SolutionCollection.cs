using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions
{
    public interface ISolutionCollection
    {
        bool IsLoading { get; }
        Task AddSolution(string solutionPath);
        bool TryGetSolutionAnalyser(SolutionModel solutionModel, out SolutionAnalyser solutionAnalyser);
        Task<List<SolutionModel>> SolutionsInCollection();
    }

    public class SolutionCollection :
        IObservable<SolutionCollection.SolutionLoadedNotification>, ISolutionCollection
    {
        private readonly ILogger<SolutionCollection> _logger;
        private readonly ISolutionAnalyserFactory _solutionAnalyserFactory;

        private readonly ConcurrentDictionary<SolutionModel, SolutionAnalyser> _solutionAnalysers = new();

        public SolutionCollection(ISolutionAnalyserFactory solutionAnalyserFactory, ILoggerFactory loggerFactory)
        {
            _solutionAnalyserFactory = solutionAnalyserFactory;
            _logger = loggerFactory.CreateLogger<SolutionCollection>();
        }

        public bool IsLoading { get; private set; }

        public async Task AddSolution(string solutionPath)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                SolutionAnalyser solution = _solutionAnalyserFactory.Create(new SolutionAnalyserOptions(solutionPath));
                await solution.LoadSolution();
                await solution.BuildSolution();

                _solutionAnalysers.TryAdd(solution.Solution, solution);

                SolutionLoadedNotify(solution.Solution);
                IsLoading = false;
            }

            else
            {
                _logger.LogWarning("Already loading a solution");
            }
        }

        public async Task<List<SolutionModel>> SolutionsInCollection()
        {
            return _solutionAnalysers
                .Select(x => x.Value.Solution)
                .OrderBy(x => x.Name)
                .ToList();
        }

        public bool TryGetSolutionAnalyser(SolutionModel solutionModel, out SolutionAnalyser solutionAnalyser)
        {
            solutionAnalyser = null!;

            if (!_solutionAnalysers.TryGetValue(solutionModel, out SolutionAnalyser? solutionAnalyserFromDict))
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