using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysis.Domain.Analyzers.CognitiveComplexity
{
    internal static class CSharpHelperExtensions
    {
        internal static int ConditionComplexityScore(this ExpressionSyntax syntax)
        {
            return syntax
                .DescendantNodesAndSelf(exp => exp is BinaryExpressionSyntax)
                .Where(exp => exp is not IdentifierNameSyntax)
                .Where(exp => exp is not LiteralExpressionSyntax)
                .Select(node => node.Kind().ToString())
                .GroupBy(name => name)
                .Count();
        }

        internal static ClassDeclarationSyntax ContainingClass(this MethodDeclarationSyntax syntax)
        {
            return syntax
                .Ancestors()
                .OfType<ClassDeclarationSyntax>()
                .First();
        }
        
        internal static NamespaceDeclarationSyntax ContainingNamespace(this ClassDeclarationSyntax syntax)
        {
            return syntax
                .Ancestors()
                .OfType<NamespaceDeclarationSyntax>()
                .First();
        }
    }
}