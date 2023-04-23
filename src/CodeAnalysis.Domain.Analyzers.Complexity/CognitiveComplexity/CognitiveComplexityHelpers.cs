using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysis.Domain.Analyzers.Complexity.CognitiveComplexity
{
    public static class CognitiveComplexityHelpers
    {
        public static bool IsAnyKind(this SyntaxNode syntaxNode, IEnumerable<SyntaxKind> syntaxKinds)
        {
            return syntaxKinds.Contains((SyntaxKind)syntaxNode.RawKind);
        }

        public static SyntaxNode RemoveParentheses(this SyntaxNode node)
        {
            var current = node;
            while (current.IsAnyKind(new[] { SyntaxKind.ParenthesizedExpression, SyntaxKind.ParenthesizedPattern }))
            {
                current = current.IsKind(SyntaxKind.ParenthesizedExpression)
                    ? ((ParenthesizedExpressionSyntax)current).Expression
                    : ((ParenthesizedPatternSyntax)current).Pattern;
            }
            return current;
        }
    }
}