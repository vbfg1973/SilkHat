using Microsoft.Extensions.Logging;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers
{
    public class SolutionAnalyserFactory(ILoggerFactory loggerFactory) : ISolutionAnalyserFactory
    {
        public SolutionAnalyser Create(SolutionAnalyserOptions solutionAnalyserOptions)
        {
            return new SolutionAnalyser(solutionAnalyserOptions, loggerFactory.CreateLogger<SolutionAnalyser>());
        }
    }
}