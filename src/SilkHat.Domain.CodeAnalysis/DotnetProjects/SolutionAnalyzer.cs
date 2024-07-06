using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Models;
using SilkHat.Domain.Common;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects
{
    public class SolutionAnalyzer : ISolutionAnalyzer
    {
        private readonly ILogger<SolutionAnalyzer> _logger;
        private readonly List<Project> _projects;
        private readonly Solution _solution;
        private readonly SolutionAnalyserOptions _solutionAnalyserOptions;
        private ConcurrentDictionary<string, Compilation> _compilations;

        public SolutionAnalyzer(SolutionAnalyserOptions solutionAnalyserOptions, ILogger<SolutionAnalyzer> logger)
        {
            _solutionAnalyserOptions = solutionAnalyserOptions;
            _logger = logger;

            _solution = LoadSolution(solutionAnalyserOptions);

            IsLoaded = BuildResults.All(x => x.DiagnosticKind != WorkspaceDiagnosticKind.Failure);
            IsBuilt = false;

            _projects = _solution.Projects.ToList();

            // _compilations = IsLoaded ? BuildIt().Result : new Dictionary<string, Compilation>();

            Solution = MapSolutionModel();
            Projects = _solution.Projects.Select(MapProjectModel).ToList();
        }

        public List<SolutionAnalyserBuildResult> BuildResults { get; } = new();
        public bool IsLoaded { get; }
        public bool IsBuilt { get; private set; }
        public SolutionModel Solution { get; init; }
        public List<ProjectModel> Projects { get; init; }

        public async Task BuildSolution()
        {
            _compilations = await BuildIt();
        }

        #region Map to public models

        private SolutionModel MapSolutionModel()
        {
            return new SolutionModel(
                PathUtilities.RemoveExtension(Path.GetFileName(_solution.FilePath!)),
                _solution.FilePath!);
        }

        private ProjectModel MapProjectModel(Project project)
        {
            return new ProjectModel(
                project.Name,
                project.FilePath!,
                project.AssemblyName
            );
        }

        #endregion

        #region Load and Build

        private Solution LoadSolution(SolutionAnalyserOptions solutionAnalyserOptions)
        {
            // Setup MSBuild
            if (!MSBuildLocator.IsRegistered) MSBuildLocator.RegisterDefaults();

            using MSBuildWorkspace workspace = MSBuildWorkspace.Create();
            workspace.WorkspaceFailed += (o, e) =>
            {
                Console.Error.WriteLine($"MSBuild {e.Diagnostic.Kind} {e.Diagnostic.Message}");
                _logger.LogError("MSBuild {MSBuildDiagnosticKind} {MSBuildDiagnosticMessage}", e.Diagnostic.Kind,
                    e.Diagnostic.Message);

                BuildResults.Add(new SolutionAnalyserBuildResult(e.Diagnostic.Kind, e.Diagnostic.Message));
            };

            return workspace.OpenSolutionAsync(solutionAnalyserOptions.SolutionPath).Result;
        }

        private async Task<ConcurrentDictionary<string, Compilation>> BuildIt()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ConcurrentDictionary<string, Compilation> compilations = new();

            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 20
            };

            await Parallel.ForEachAsync(_solution.Projects, parallelOptions, async (project, token) =>
            {
                await Console.Error.WriteLineAsync($"Building: {Solution.Name} - {project.Name}");
                Compilation? compilation = await project.GetCompilationAsync(token);

                if (compilation != null)
                {
                    compilations.TryAdd(project.Name, compilation);
                    await Console.Error.WriteLineAsync($"Finished Building: {Solution.Name} - {project.Name}");
                }
            });

            sw.Stop();

            await Console.Error.WriteLineAsync($"Finished Building Solution {Solution.Name} - {sw.Elapsed}");
            IsBuilt = true;
            return compilations;
        }

        #endregion
    }
}