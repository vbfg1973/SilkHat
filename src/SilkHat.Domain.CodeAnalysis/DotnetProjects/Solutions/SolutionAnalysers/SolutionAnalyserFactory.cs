using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.AST;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers
{
    public class SolutionAnalyserFactory(
        IProjectStructureBuilder projectStructureBuilder,
        ISyntaxStructureBuilder syntaxStructureBuilder,
        ILoggerFactory loggerFactory)
        : ISolutionAnalyserFactory
    {
        public SolutionAnalyser Create(SolutionAnalyserOptions solutionAnalyserOptions)
        {
            return new SolutionAnalyser(
                solutionAnalyserOptions,
                projectStructureBuilder,
                syntaxStructureBuilder,
                loggerFactory.CreateLogger<SolutionAnalyser>());
        }
    }
}