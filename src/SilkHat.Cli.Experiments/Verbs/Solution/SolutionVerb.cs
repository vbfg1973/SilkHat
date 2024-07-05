using CommandLine;
using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.Solutions;

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

            var solutionAnalyzer = solutionAnalyserFactory.Create(new SolutionAnalyserOptions(options.Solution));

            foreach (var project in solutionAnalyzer.Projects)
            {
                Console.WriteLine(project.AssemblyName);
            }
        }
    }
}