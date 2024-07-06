using Microsoft.Extensions.Logging;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects
{
    public class SolutionAnalyserFactory(ILoggerFactory loggerFactory) : ISolutionAnalyserFactory
    {
        public SolutionAnalyzer Create(SolutionAnalyserOptions solutionAnalyserOptions)
        {
            return new SolutionAnalyzer(solutionAnalyserOptions, loggerFactory.CreateLogger<SolutionAnalyzer>());
        }
    }
}