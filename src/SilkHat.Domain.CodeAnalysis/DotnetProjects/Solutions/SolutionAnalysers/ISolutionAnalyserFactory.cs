namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers
{
    public interface ISolutionAnalyserFactory
    {
        SolutionAnalyser Create(SolutionAnalyserOptions solutionAnalyserOptions);
    }
}