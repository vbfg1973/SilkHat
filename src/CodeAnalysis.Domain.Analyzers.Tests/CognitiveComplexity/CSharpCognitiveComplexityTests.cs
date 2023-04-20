using CodeAnalysis.Domain.Analyzers.CognitiveComplexity;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysis.Domain.Analyzers.Tests.CognitiveComplexity
{
    public class CSharpCognitiveComplexityTests
    {
        [Theory]
        [InlineData("CSharpIfElseClass.cs", "BasicMethod", 0)]
        [InlineData("CSharpIfElseClass.cs", "Method_CoalescedIfElse", 0)]
        // [InlineData("CSharpIfElseClass.cs", "Method_IfElseStatement", 2)]
        // [InlineData("CSharpIfElseClass.cs", "Method_IfElseIfStatement", 3)]
        // [InlineData("CSharpIfElseClass.cs", "Method_NestedIfElseStatement", 5)]
        // [InlineData("CSharpIfElseClass.cs", "Method_DoublyNestedIfElseStatement", 8)]
        // [InlineData("CSharpIfElseClass.cs", "Method_DeeplyNestedIfElseStatement", 12)]
        public void GivenClassMethodHasCorrectCognitiveComplexity(string fileName, string methodName,
            int expectedComplexity)
        {
            var treeRoot = Helpers.ParseCSharpSyntaxTreeRoot(Path.Combine("TestClasses", fileName));

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