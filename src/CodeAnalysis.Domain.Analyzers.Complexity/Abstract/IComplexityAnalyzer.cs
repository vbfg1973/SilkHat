namespace CodeAnalysis.Domain.Analyzers.Complexity.Abstract
{
    public interface IComplexityAnalyzer
    {
        int ComplexityScore { get; }
    }
}