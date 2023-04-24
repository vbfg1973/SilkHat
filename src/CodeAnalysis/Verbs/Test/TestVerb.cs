using System.Text.Json;
using CodeAnalysis.Domain.Analyzers.Complexity.IndentationComplexity;
using CodeAnalysis.Domain.Extensions;
using CodeAnalysis.Verbs.Abstract;
using Microsoft.Extensions.Logging;

namespace CodeAnalysis.Verbs.Test
{
    public class TestOptions : IPathBasedOptions
    {
        public string DirectoryPath { get; set; } = null!;
        public string OutputCsv { get; set; } = null!;
    }

    public record ComplexityResult
    {
        public string File { get; set; }
        public int ComplexityScore { get; set; }
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
            var results = GetComplexities(options.DirectoryPath, new[] { ".vb", ".cs" }).ToList();
            await CsvUtilities.CsvWriteAsync(options.OutputCsv, results);
        }

        private static IEnumerable<ComplexityResult> GetComplexities(string dirPath, string[] extensions)
        {
            return FileUtilities.FindFiles(dirPath, new[] { ".cs", ".vb" })
                .Select(path => FileToComplexityResult(dirPath, path));
        }

        private static ComplexityResult FileToComplexityResult(string dirPath, string path)
        {
            var text = File.ReadAllText(path);
            var complexityAnalyzer = new IndentationComplexityAnalyzer(text);

            return new ComplexityResult()
            {
                File = Path.GetRelativePath(dirPath, path),
                ComplexityScore = complexityAnalyzer.ComplexityScore
            };
        }
    }
}