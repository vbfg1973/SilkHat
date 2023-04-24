namespace CodeAnalysis.Domain.Analyzers.Complexity.Abstract
{
    /// <summary>
    ///     Generic interface for a complexity analyzer
    /// </summary>
    public interface IComplexityAnalyzer
    {
        int ComplexityScore { get; }
    }
}