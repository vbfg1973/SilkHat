using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace CodeAnalysis.Domain.Analyzers.CognitiveComplexity
{
    internal static class VisualBasicHelperExtensions
    {
        internal static int ConditionComplexityScore(this ExpressionSyntax expressionSyntax)
        {
            return expressionSyntax
                .DescendantNodesAndSelf(exp => exp is BinaryExpressionSyntax)
                .Where(exp => exp is not IdentifierNameSyntax)
                .Where(exp => exp is not LiteralExpressionSyntax)
                .Select(node => node.Kind().ToString())
                .GroupBy(name => name)
                .Count();
        }
    }
}