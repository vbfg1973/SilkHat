using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Models;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects
{
    public class SolutionAnalyzer : ISolutionAnalyzer
    {
        private readonly Dictionary<string, Compilation> _compilations;
        private readonly ILogger<SolutionAnalyzer> _logger;
        private readonly List<Project> _projects;
        private readonly Solution _solution;
        private readonly SolutionAnalyserOptions _solutionAnalyserOptions;

        public SolutionAnalyzer(SolutionAnalyserOptions solutionAnalyserOptions, ILogger<SolutionAnalyzer> logger)
        {
            _solutionAnalyserOptions = solutionAnalyserOptions;
            _logger = logger;

            _solution = LoadSolution(solutionAnalyserOptions);

            IsLoaded = BuildResults.All(x => x.DiagnosticKind != WorkspaceDiagnosticKind.Failure);

            _projects = _solution.Projects.ToList();

            _compilations = IsLoaded ? BuildIt().Result : new Dictionary<string, Compilation>();

            Solution = MapSolutionModel();
            Projects = _solution.Projects.Select(MapProjectModel).ToList();
        }

        public List<SolutionAnalyserBuildResult> BuildResults { get; } = new();
        public bool IsLoaded { get; }
        public SolutionModel Solution { get; init; }
        public List<ProjectModel> Projects { get; init; }

        #region Map to public models

        private SolutionModel MapSolutionModel()
        {
            return new SolutionModel(
                Path.ChangeExtension(Path.GetFileName(_solution.FilePath!), ""),
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

        private async Task<Dictionary<string, Compilation>> BuildIt()
        {
            Dictionary<string, Compilation> compilations = new();

            foreach (Project project in _solution.Projects)
            {
                await Console.Error.WriteLineAsync($"Building: {project.Name}");
                Compilation? compilation = await project.GetCompilationAsync();

                if (compilation == null) continue;

                compilations[project.Name] = compilation;
            }

            return compilations;
        }

        #endregion
    }
}