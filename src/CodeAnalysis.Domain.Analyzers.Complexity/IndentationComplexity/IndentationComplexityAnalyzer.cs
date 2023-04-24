using CodeAnalysis.Domain.Analyzers.Complexity.Abstract;

namespace CodeAnalysis.Domain.Analyzers.Complexity.IndentationComplexity
{
    public class IndentationComplexityAnalyzer : IIndentationComplexityAnalyzer
    {
        public IndentationComplexityAnalyzer(IEnumerable<string> lines)
        {
            var linesArray = lines.CleanLinesArray();
            ComplexityScore = linesArray.Sum(str => str.LeadingWhitespaceCount());
        }

        public IndentationComplexityAnalyzer(string text)
        {
            var linesArray = text.Split("\n").CleanLinesArray();
            ComplexityScore = linesArray.Sum(str => str.LeadingWhitespaceCount());
        }

        public int ComplexityScore { get; }
    }
}