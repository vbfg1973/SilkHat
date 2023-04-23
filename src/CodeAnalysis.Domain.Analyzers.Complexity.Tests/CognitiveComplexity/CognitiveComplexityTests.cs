using CodeAnalysis.Domain.Analyzers.Complexity.CognitiveComplexity;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using CognitiveComplexityHelpers = CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities.CognitiveComplexityHelpers;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity
{
    public class CognitiveComplexityTests
    {
        [Theory]
        [ClassData(typeof(CatchClauseCSharp))]
        [ClassData(typeof(CatchClauseVisualBasic))]
        [ClassData(typeof(BinaryExpressionCSharp))]
        [ClassData(typeof(BinaryExpressionVisualBasic))]
        [ClassData(typeof(DoWhileLoopCSharp))]
        [ClassData(typeof(DoWhileLoopVisualBasic))]
        [ClassData(typeof(ForeachLoopCSharp))]
        [ClassData(typeof(ForeachLoopVisualBasic))]
        [ClassData(typeof(ForLoopCSharp))]
        [ClassData(typeof(ForLoopVisualBasic))]
        [ClassData(typeof(GotoCSharp))]
        [ClassData(typeof(GotoVisualBasic))]
        [ClassData(typeof(IfElseCSharp))]
        [ClassData(typeof(IfElseVisualBasic))]
        [ClassData(typeof(LambdaCSharp))]
        [ClassData(typeof(LambdaVisualBasic))]
        [ClassData(typeof(SwitchCSharp))]
        [ClassData(typeof(SwitchVisualBasic))]
        [ClassData(typeof(WhileLoopCSharp))]
        [ClassData(typeof(WhileLoopVisualBasic))]
        public void GivenClassMethodHasCorrectCognitiveComplexity(string fileName, string methodName,
            int expectedComplexityScore, Language language)
        {
            var treeRoot = CognitiveComplexityHelpers.ParseSyntaxTreeRoot(Path.Combine("TestClasses", string.Join(".", fileName)),
                language);

            var complexityScore = GetComplexityScore(treeRoot, methodName, language);

            complexityScore
                .Should()
                .Be(expectedComplexityScore);
        }

        private static int GetComplexityScore(SyntaxNode syntaxNode, string methodName, Language language)
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

            var analyzer = new CSharpCognitiveComplexityAnalyzer(methodDeclarationSyntax);

            return analyzer.ComplexityScore;
        }

        private static int GetVisualBasicComplexityScore(SyntaxNode syntaxNode, string methodName)
        {
            var methodDeclarationSyntax = syntaxNode
                .DescendantNodes()
                .OfType<MethodBlockSyntax>()
                .First(x => x.SubOrFunctionStatement.Identifier.ToString() == methodName);

            var analyzer = new VisualBasicCognitiveComplexityAnalyzer(methodDeclarationSyntax);

            return analyzer.ComplexityScore;
        }
    }
}