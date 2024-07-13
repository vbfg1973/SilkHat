using Microsoft.CodeAnalysis;

namespace SilkHat.Domain.CodeAnalysis.Abstract
{
    /// <summary>
    ///     Generic interface for dotnet complexity analyzers based on methods
    /// </summary>
    public interface IDotnetComplexityAnalyzer : IComplexityAnalyzer
    {
        string MethodName { get; }
        string? ContainingClassName { get; }
        string? ContainingNamespace { get; }
        Location Location { get; }
        List<Location> Locations { get; }
    }
}