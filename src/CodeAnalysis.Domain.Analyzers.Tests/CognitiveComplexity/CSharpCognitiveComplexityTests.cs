using CodeAnalysis.Domain.Analyzers.CognitiveComplexity;
using CodeAnalysis.Domain.Analyzers.Tests.TestClasses;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysis.Domain.Analyzers.Tests.CognitiveComplexity
{
    public class CSharpCognitiveComplexityTests
    {
        [Theory]
        [InlineData(nameof(CSharpIfElseClass), nameof(CSharpIfElseClass.BasicMethod), 0)]
        [InlineData(nameof(CSharpIfElseClass), nameof(CSharpIfElseClass.Method_CoalescedIfElse), 0)]
        [InlineData(nameof(CSharpIfElseClass), nameof(CSharpIfElseClass.Method_IfStatement), 1)]
        [InlineData(nameof(CSharpIfElseClass), nameof(CSharpIfElseClass.Method_IfElseStatement), 2)]
        [InlineData(nameof(CSharpIfElseClass), nameof(CSharpIfElseClass.Method_IfElseIfStatement), 3)]
        [InlineData(nameof(CSharpIfElseClass), nameof(CSharpIfElseClass.Method_NestedIfElseStatement), 5)]
        [InlineData(nameof(CSharpIfElseClass), nameof(CSharpIfElseClass.Method_DoublyNestedIfElseStatement), 8)]
        [InlineData(nameof(CSharpIfElseClass), nameof(CSharpIfElseClass.Method_DeeplyNestedIfElseStatement), 12)]
        public void GivenClassMethodHasCorrectCognitiveComplexity(string className, string methodName,
            int expectedComplexity)
        {
            var treeRoot = Helpers.ParseCSharpSyntaxTreeRoot(Path.Combine("TestClasses", string.Join(".", className, "cs")));

            var methodDeclarationSyntax = treeRoot
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .First(x => x.Identifier.ToString() == methodName);

            var analyzer = new CSharpCognitiveComplexityMethodAnalyzer(methodDeclarationSyntax);

            analyzer.ComplexityScore
                .Should()
                .Be(expectedComplexity);
        }
    }
}