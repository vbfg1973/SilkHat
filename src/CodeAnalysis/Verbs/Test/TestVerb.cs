using System.Text.Json;
using CodeAnalysis.Domain.Analyzers.Complexity.Abstract;
using CodeAnalysis.Domain.Analyzers.Complexity.CognitiveComplexity;
using CodeAnalysis.Domain.Extensions;
using CodeAnalysis.Verbs.Abstract;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Extensions.Logging;

namespace CodeAnalysis.Verbs.Test
{
    public class TestOptions : IPathBasedOptions
    {
        public string DirectoryPath { get; set; } = null!;
    }

    public class TestVerb
    {
        private readonly ILogger<TestVerb> _logger;

        public TestVerb(ILogger<TestVerb> logger)
        {
            _logger = logger;
        }

        public async Task Run(IPathBasedOptions options)
        {
            Console.WriteLine(JsonSerializer.Serialize(options));

            var files = FileUtilities.FindFiles(options.DirectoryPath, new[] { ".cs", ".vb" }).ToList();

            Console.WriteLine($"FileCount={files.Count}");

            try
            {
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file);

                    switch (extension)
                    {
                        case ".vb":
                            await VisualBasicComplexity(options.DirectoryPath, file, Language.VisualBasic);
                            break;
                        case ".cs":
                            await CSharpComplexity(options.DirectoryPath, file, Language.CSharp);
                            break;
                    }
                }
            }

            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        private async Task VisualBasicComplexity(string dir, string fileName, Language language)
        {
            var root = SyntaxTreeHelpers.ParseSyntaxTreeRoot(fileName, language);

            foreach (var method in GetVisualBasicMethods(root))
            {
                var complexityAnalyzer = GetComplexityAnalyzer(Language.VisualBasic, method);
                Console.WriteLine(string.Join("\t",
                    Path.GetRelativePath(dir, fileName.Trim()),
                    complexityAnalyzer.ContainingNamespace?.Trim(),
                    complexityAnalyzer.ContainingClassName?.Trim(),
                    complexityAnalyzer.Name?.Trim(),
                    complexityAnalyzer.ComplexityScore));
            }
        }

        private async Task CSharpComplexity(string dir, string fileName, Language language)
        {
            var root = SyntaxTreeHelpers.ParseSyntaxTreeRoot(fileName, language);

            foreach (var method in GetCSharpMethods(root))
            {
                var complexityAnalyzer = GetComplexityAnalyzer(Language.CSharp, method);
                Console.WriteLine(string.Join("\t",
                    Path.GetRelativePath(dir, fileName.Trim()),
                    complexityAnalyzer.ContainingNamespace?.Trim(),
                    complexityAnalyzer.ContainingClassName?.Trim(),
                    complexityAnalyzer.Name?.Trim(),
                    complexityAnalyzer.ComplexityScore));
            }
        }

        private IEnumerable<MethodDeclarationSyntax> GetCSharpMethods(SyntaxNode syntaxNode)
        {
            return syntaxNode
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>();
        }

        private IEnumerable<MethodBlockSyntax> GetVisualBasicMethods(SyntaxNode syntaxNode)
        {
            return syntaxNode
                .DescendantNodes()
                .OfType<MethodBlockSyntax>();
        }

        private IDotnetComplexityAnalyzer GetComplexityAnalyzer(Language language, SyntaxNode syntaxNode)
        {
            return language switch
            {
                Language.CSharp => new CSharpCognitiveComplexityAnalyzer((MethodDeclarationSyntax)syntaxNode),
                Language.VisualBasic => new VisualBasicCognitiveComplexityAnalyzer((MethodBlockSyntax)syntaxNode),
                _ => throw new ArgumentOutOfRangeException(language.ToString())
            };
        }
    }
}