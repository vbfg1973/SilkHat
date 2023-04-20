using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysis.Domain.Analyzers.Abstract
{
    public interface IMethodAnalyzer
    {
        string Name { get; }
        string ContainingClassName { get; }
        string ContainingNamespace { get; }
        int ComplexityScore { get; }
        int Analyze(MethodDeclarationSyntax methodSyntax);
    }
}