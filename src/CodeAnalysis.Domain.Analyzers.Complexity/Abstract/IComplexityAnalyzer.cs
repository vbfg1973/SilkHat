using Microsoft.CodeAnalysis;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Abstract
{
    public interface IComplexityAnalyzer
    {
        string Name { get; }
        string ContainingClassName { get; }
        string ContainingNamespace { get; }
        int ComplexityScore { get; }
        Location Location { get; }
        List<Location> Locations { get; }
    }
}