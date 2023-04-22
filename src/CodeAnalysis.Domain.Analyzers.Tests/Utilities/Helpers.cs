using CodeAnalysis.Domain.Analyzers.Abstract;
using CodeAnalysis.Domain.Analyzers.CognitiveComplexity;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace CodeAnalysis.Domain.Analyzers.Tests.Utilities
{
    public static class Helpers
    {
        public static SyntaxNode ParseSyntaxTreeRoot(string path, Language language)
        {
            var text = File.ReadAllText(path);

            return language switch
            {
                Language.CSharp => CSharpSyntaxTree.ParseText(text).GetRoot(),
                Language.VisualBasic => VisualBasicSyntaxTree.ParseText(text).GetRoot(),
                _ => throw new ArgumentException("Unknown language", nameof(language))
            };
        }

        public static MethodDeclarationSyntax FindMethod(CSharpSyntaxNode syntaxNode, string methodName)
        {
            return syntaxNode
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .First(method => method.Identifier.ToString() == methodName);
        }

        public static MethodBlockSyntax FindMethod(VisualBasicSyntaxNode syntaxNode, string methodName)
        {
            return syntaxNode
                .DescendantNodes()
                .OfType<MethodBlockSyntax>()
                .First(method => method.SubOrFunctionStatement.Identifier.ToString() == methodName);
        }

        public static IMethodAnalyzer GetMethodAnalyzer(Language language, SyntaxNode syntaxNode)
        {
            return language switch
            {
                Language.CSharp => new CSharpCognitiveComplexityMethodAnalyzer((MethodDeclarationSyntax)syntaxNode),
                Language.VisualBasic => new VisualBasicCognitiveComplexityMethodAnalyzer((MethodBlockSyntax)syntaxNode),
                _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
            };
        }

        public static string FileNameFromClass(string className)
        {
            return string.Join(".", className, "cs");
        }
    }
}