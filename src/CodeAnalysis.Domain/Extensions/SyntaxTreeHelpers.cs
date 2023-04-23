using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.VisualBasic;

namespace CodeAnalysis.Domain.Extensions
{
    public class SyntaxTreeHelpers
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
    }
}