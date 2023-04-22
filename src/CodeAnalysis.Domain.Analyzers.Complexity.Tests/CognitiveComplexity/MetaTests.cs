// using CodeAnalysis.Domain.Analyzers.Complexity.CognitiveComplexity;
// using CodeAnalysis.Domain.Analyzers.Complexity.Tests.TestClasses;
// using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;
// using FluentAssertions;
// using Microsoft.CodeAnalysis.CSharp.Syntax;
//
// namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity
// {
//     public class MetaTests
//     {
//         [Theory]
//         [InlineData("CSharpIfElseClass.cs", nameof(CSharpIfElseClass.BasicMethod), nameof(CSharpIfElseClass),
//             Language.CSharp)]
//         [InlineData("CSharpIfElseClass.cs", nameof(CSharpIfElseClass.Method_IfElseStatement), nameof(CSharpIfElseClass),
//             Language.CSharp)]
//         public void GivenClassFileWithNamedMethodsIdentifiesClassName(string fileName, string methodName,
//             string expectedClassName, Language language)
//         {
//             var treeRoot = Utilities.Helpers.ParseSyntaxTreeRoot(Path.Combine("TestClasses", fileName), language);
//
//             var method = treeRoot
//                 .DescendantNodes()
//                 .OfType<MethodDeclarationSyntax>()
//                 .First(method => method.Identifier.ToString() == methodName);
//
//             var analyzer = new CSharpCognitiveComplexityMethodAnalyzer(method);
//
//             analyzer
//                 .ContainingClassName
//                 .Should()
//                 .Be(expectedClassName);
//         }
//
//         [Theory]
//         [InlineData("CSharpIfElseClass.cs", nameof(CSharpIfElseClass.BasicMethod),
//             "CodeAnalysis.Domain.Analyzers.Complexity.Tests.TestClasses", Language.CSharp)]
//         [InlineData("CSharpIfElseClass.cs", nameof(CSharpIfElseClass.Method_IfElseStatement),
//             "CodeAnalysis.Domain.Analyzers.Complexity.Tests.TestClasses", Language.CSharp)]
//         public void GivenClassFileWithNamedMethodsIdentifiesNamespaceName(string fileName, string methodName,
//             string expectedNamespaceName, Language language)
//         {
//             var treeRoot = Utilities.Helpers.ParseSyntaxTreeRoot(Path.Combine("TestClasses", fileName), language);
//
//             var method = treeRoot
//                 .DescendantNodes()
//                 .OfType<MethodDeclarationSyntax>()
//                 .First(method => method.Identifier.ToString() == methodName);
//
//             var analyzer = new CSharpCognitiveComplexityMethodAnalyzer(method);
//
//             analyzer
//                 .ContainingNamespace
//                 .Should()
//                 .Be(expectedNamespaceName);
//         }
//     }
// }

