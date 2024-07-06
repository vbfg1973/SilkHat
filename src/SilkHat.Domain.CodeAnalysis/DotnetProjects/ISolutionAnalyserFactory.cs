namespace SilkHat.Domain.CodeAnalysis.DotnetProjects
{
    public interface ISolutionAnalyserFactory
    {
        SolutionAnalyser Create(SolutionAnalyserOptions solutionAnalyserOptions);
    }
}