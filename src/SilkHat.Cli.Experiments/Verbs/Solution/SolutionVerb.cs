using CommandLine;
using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.DotnetProjects;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Models;

namespace SilkHat.Cli.Experiments.Verbs.Solution
{
    [Verb("solution")]
    public class SolutionOptions
    {
        [Option('s', nameof(Solution), Required = true)]
        public string Solution { get; set; } = null!;
    }

    public class SolutionVerb(ISolutionAnalyserFactory solutionAnalyserFactory, ILogger<SolutionVerb> logger)
    {
        public async Task Run(SolutionOptions options)
        {
            await Console.Error.WriteLineAsync(options.Solution);

            SolutionAnalyzer solutionAnalyzer =
                solutionAnalyserFactory.Create(new SolutionAnalyserOptions(options.Solution));

            Console.WriteLine($"Build results: {solutionAnalyzer.BuildResults.Count}");
            foreach (SolutionAnalyserBuildResult buildResult in solutionAnalyzer.BuildResults)
            {
                Console.WriteLine($"\t{buildResult.DiagnosticKind} - {buildResult.Message}");
            }

            foreach (ProjectModel project in solutionAnalyzer.Projects)
            {
                Console.WriteLine(project);
            }
        }
    }
}