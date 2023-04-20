using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeAnalysis.Domain.Analyzers.Tests
{
    public static class Helpers
    {
        public static SyntaxNode ParseCSharpSyntaxTreeRoot(string path)
        {
            var text = File.ReadAllText(path);
            return CSharpSyntaxTree.ParseText(text).GetRoot();   
        }
    }
}