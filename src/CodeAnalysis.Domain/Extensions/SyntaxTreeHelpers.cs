using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.VisualBasic;

namespace CodeAnalysis.Domain.Extensions
{
    /// <summary>
    ///     Helpers for parsing and accessing syntax trees
    /// </summary>
    public class SyntaxTreeHelpers
    {
        /// <summary>
        ///     Parses source code of the nominated type to a Roslyn Syntax Tree
        /// </summary>
        /// <param name="path"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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