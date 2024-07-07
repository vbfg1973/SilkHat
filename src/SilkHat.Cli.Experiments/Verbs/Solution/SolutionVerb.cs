using CommandLine;
using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.DotnetProjects;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Models;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers;

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

            SolutionAnalyser solutionAnalyser =
                solutionAnalyserFactory.Create(new SolutionAnalyserOptions(options.Solution));

            Console.WriteLine($"Build results: {solutionAnalyser.BuildResults.Count}");
            foreach (SolutionAnalyserBuildResult buildResult in solutionAnalyser.BuildResults)
            {
                Console.WriteLine($"\t{buildResult.DiagnosticKind} - {buildResult.Message}");
            }

            foreach (ProjectModel project in solutionAnalyser.Projects)
            {
                Console.WriteLine(project);
            }
        }
    }
}