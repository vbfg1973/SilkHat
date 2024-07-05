using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Logging;

namespace SilkHat.Domain.CodeAnalysis.Solutions
{
    public class SolutionAnalyzer
    {
        private readonly ILogger<SolutionAnalyzer> _logger;
        private readonly SolutionAnalyserOptions _solutionAnalyserOptions;

        public SolutionAnalyzer(SolutionAnalyserOptions solutionAnalyserOptions, ILogger<SolutionAnalyzer> logger)
        {
            _solutionAnalyserOptions = solutionAnalyserOptions;
            _logger = logger;

            // Setup MSBuild
            if (!MSBuildLocator.IsRegistered) MSBuildLocator.RegisterDefaults();

            using MSBuildWorkspace workspace = MSBuildWorkspace.Create();
            workspace.WorkspaceFailed += (o, e) =>
            {
                Console.Error.WriteLine($"MSBuild {e.Diagnostic.Kind} {e.Diagnostic.Message}");
                _logger.LogError("MSBuild {MSBuildDiagnosticKind} {MSBuildDiagnosticMessage}", e.Diagnostic.Kind,
                    e.Diagnostic.Message);
            };

            Solution = workspace.OpenSolutionAsync(solutionAnalyserOptions.SolutionPath).Result;
        }

        public Solution Solution { get; init; }

        public IEnumerable<Project> Projects => Solution.Projects;

        public Dictionary<string, Compilation> Compilations { get; } = new();

        private async Task BuildIt()
        {
            foreach (Project project in Solution.Projects)
            {
                await Console.Error.WriteLineAsync($"Building: {project.Name}");
                Compilation? compilation = await project.GetCompilationAsync();

                if (compilation == null) continue;

                Compilations[project.Name] = compilation;
            }
        }
    }
}