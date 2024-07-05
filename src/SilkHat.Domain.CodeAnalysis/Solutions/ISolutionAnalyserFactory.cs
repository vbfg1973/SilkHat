namespace SilkHat.Domain.CodeAnalysis.Solutions
{
    public interface ISolutionAnalyserFactory
    {
        SolutionAnalyzer Create(SolutionAnalyserOptions solutionAnalyserOptions);
    }
}