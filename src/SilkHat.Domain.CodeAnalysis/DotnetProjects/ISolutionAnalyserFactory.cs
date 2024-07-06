namespace SilkHat.Domain.CodeAnalysis.DotnetProjects
{
    public interface ISolutionAnalyserFactory
    {
        SolutionAnalyzer Create(SolutionAnalyserOptions solutionAnalyserOptions);
    }
}