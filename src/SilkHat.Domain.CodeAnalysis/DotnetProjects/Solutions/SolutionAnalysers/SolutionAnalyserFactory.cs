using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers
{
    public class SolutionAnalyserFactory(IProjectStructureBuilder projectStructureBuilder, ILoggerFactory loggerFactory)
        : ISolutionAnalyserFactory
    {
        public SolutionAnalyser Create(SolutionAnalyserOptions solutionAnalyserOptions)
        {
            return new SolutionAnalyser(solutionAnalyserOptions, projectStructureBuilder,
                loggerFactory.CreateLogger<SolutionAnalyser>());
        }
    }
}