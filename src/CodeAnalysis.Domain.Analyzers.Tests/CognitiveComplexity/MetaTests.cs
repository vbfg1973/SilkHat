using CodeAnalysis.Domain.Analyzers.CognitiveComplexity;
using CodeAnalysis.Domain.Analyzers.Tests.TestClasses;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysis.Domain.Analyzers.Tests.CognitiveComplexity
{
    public class MetaTests
    {
        [Theory]
        [InlineData("CSharpIfElseClass.cs", nameof(CSharpIfElseClass.BasicMethod), nameof(CSharpIfElseClass))]
        [InlineData("CSharpIfElseClass.cs", nameof(CSharpIfElseClass.Method_IfElseStatement), nameof(CSharpIfElseClass))]
        [InlineData("CSharpLogicalExpressionClass.cs", nameof(CSharpLogicalExpressionClass.BasicLogic), nameof(CSharpLogicalExpressionClass))]
        public void GivenClassFileWithNamedMethodsIdentifiesClassName(string fileName, string methodName, string expectedClassName)
        {
            var treeRoot = Helpers.ParseCSharpSyntaxTreeRoot(Path.Combine("TestClasses", fileName));

            var method = treeRoot
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .First(method => method.Identifier.ToString() == methodName);

            var analyzer = new CSharpCognitiveComplexityMethodAnalyzer(method);

            analyzer
                .ContainingClassName
                .Should()
                .Be(expectedClassName);
        }
        
        [Theory]
        [InlineData("CSharpIfElseClass.cs", nameof(CSharpIfElseClass.BasicMethod), "CodeAnalysis.Domain.Analyzers.Tests.TestClasses")]
        [InlineData("CSharpIfElseClass.cs", nameof(CSharpIfElseClass.Method_IfElseStatement), "CodeAnalysis.Domain.Analyzers.Tests.TestClasses")]
        [InlineData("CSharpLogicalExpressionClass.cs", nameof(CSharpLogicalExpressionClass.BasicLogic), "CodeAnalysis.Domain.Analyzers.Tests.TestClasses")]
        public void GivenClassFileWithNamedMethodsIdentifiesNamespaceName(string fileName, string methodName, string expectedNamespaceName)
        {
            var treeRoot = Helpers.ParseCSharpSyntaxTreeRoot(Path.Combine("TestClasses", fileName));

            var method = treeRoot
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .First(method => method.Identifier.ToString() == methodName);

            var analyzer = new CSharpCognitiveComplexityMethodAnalyzer(method);

            analyzer
                .ContainingNamespace
                .Should()
                .Be(expectedNamespaceName);
        }
    }
}