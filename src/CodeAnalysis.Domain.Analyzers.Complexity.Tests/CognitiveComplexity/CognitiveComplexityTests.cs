using CodeAnalysis.Domain.Analyzers.Complexity.CognitiveComplexity;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity
{
    public class CognitiveComplexityTests
    {
        [Theory]
        #region If/Else
        [ClassData(typeof(IfElseCSharp))]
        [ClassData(typeof(IfElseDataVisualBasic))]
        #endregion
        #region For
        [ClassData(typeof(ForCSharp))]
        [ClassData(typeof(ForVisualBasic))]
        #endregion
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