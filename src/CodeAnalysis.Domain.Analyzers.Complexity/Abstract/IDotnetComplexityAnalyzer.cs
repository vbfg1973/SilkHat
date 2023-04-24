using Microsoft.CodeAnalysis;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Abstract
{
    public interface IDotnetComplexityAnalyzer : IComplexityAnalyzer
    {
        string Name { get; }
        string? ContainingClassName { get; }
        string? ContainingNamespace { get; }
        Location Location { get; }
        List<Location> Locations { get; }
    }
}