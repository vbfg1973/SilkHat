using CodeAnalysis.Domain.Analyzers.CognitiveComplexity;
using CodeAnalysis.Domain.Analyzers.Tests.Utilities;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace CodeAnalysis.Domain.Analyzers.Tests.CognitiveComplexity
{
    public class CognitiveComplexityTests
    {
        [Theory]
        [InlineData("CSharpIfElseClass.CSharp", "BasicMethod", 0, Language.CSharp)]
        [InlineData("CSharpIfElseClass.CSharp", "Method_CoalescedIfElse", 0, Language.CSharp)]
        [InlineData("CSharpIfElseClass.CSharp", "Method_IfStatement", 1, Language.CSharp)]
        [InlineData("CSharpIfElseClass.CSharp", "Method_IfElseStatement", 2, Language.CSharp)]
        [InlineData("CSharpIfElseClass.CSharp", "Method_IfElseIfStatement", 3, Language.CSharp)]
        [InlineData("CSharpIfElseClass.CSharp", "Method_NestedIfElseStatement", 5, Language.CSharp)]
        [InlineData("CSharpIfElseClass.CSharp", "Method_DoublyNestedIfElseStatement", 8, Language.CSharp)]
        [InlineData("CSharpIfElseClass.CSharp", "Method_DeeplyNestedIfElseStatement", 12, Language.CSharp)]
        [InlineData("VisualBasicIfElseClass.VisualBasic", "BasicMethod", 0, Language.VisualBasic)]
        [InlineData("VisualBasicIfElseClass.VisualBasic", "Method_CoalescedIfElse", 0, Language.VisualBasic)]
        [InlineData("VisualBasicIfElseClass.VisualBasic", "Method_IfStatement", 1, Language.VisualBasic)]
        [InlineData("VisualBasicIfElseClass.VisualBasic", "Method_IfElseStatement", 2, Language.VisualBasic)]
        [InlineData("VisualBasicIfElseClass.VisualBasic", "Method_IfElseIfStatement", 3, Language.VisualBasic)]
        [InlineData("VisualBasicIfElseClass.VisualBasic", "Method_NestedIfElseStatement", 5, Language.VisualBasic)]
        [InlineData("VisualBasicIfElseClass.VisualBasic", "Method_DoublyNestedIfElseStatement", 8, Language.VisualBasic)]
        [InlineData("VisualBasicIfElseClass.VisualBasic", "Method_DeeplyNestedIfElseStatement", 12, Language.VisualBasic)]
        public void GivenClassMethodHasCorrectCognitiveComplexity(string fileName, string methodName,
            int expectedComplexityScore, Language language)
        {
            var treeRoot = Helpers.ParseSyntaxTreeRoot(Path.Combine("TestClasses", string.Join(".", fileName)),
                language);

            var complexityScore = GetComplexityScore(treeRoot, methodName, language);

            complexityScore
                .Should()
                .Be(expectedComplexityScore);
        }

        private int GetComplexityScore(SyntaxNode syntaxNode, string methodName, Language language)
        {
            return language switch
            {
                Language.CSharp => GetCSharpComplexityScore(syntaxNode, methodName),
                Language.VisualBasic => GetVisualBasicComplexityScore(syntaxNode, methodName),
                _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
            };
        }

        private static int GetCSharpComplexityScore(SyntaxNode syntaxNode, string methodName)
        {
            var methodDeclarationSyntax = syntaxNode
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .First(x => x.Identifier.ToString() == methodName);

            var analyzer = new CSharpCognitiveComplexityMethodAnalyzer(methodDeclarationSyntax);

            return analyzer.ComplexityScore;
        }
        
        private static int GetVisualBasicComplexityScore(SyntaxNode syntaxNode, string methodName)
        {
            var methodDeclarationSyntax = syntaxNode
                .DescendantNodes()
                .OfType<MethodBlockSyntax>()
                .First(x => x.SubOrFunctionStatement.Identifier.ToString() == methodName);

            var analyzer = new VisualBasicCognitiveComplexityMethodAnalyzer(methodDeclarationSyntax);

            return analyzer.ComplexityScore;
        }
    }
}