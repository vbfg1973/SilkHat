using System.Text.RegularExpressions;
using CodeAnalysis.Domain.Analyzers.Complexity.Abstract;

namespace CodeAnalysis.Domain.Analyzers.Complexity.IndentationComplexity
{
    public class IndentationComplexityAnalyzer : IIndentationComplexityAnalyzer
    {
        private readonly string[] _linesArray;
        public int ComplexityScore { get; private set; }

        public IndentationComplexityAnalyzer(IEnumerable<string> lines)
        {
            _linesArray = lines.CleanLinesArray();
            ComplexityScore = _linesArray.Sum(CountLeadingWhitespaceIteration);
        }
        
        public IndentationComplexityAnalyzer(string text)
        {
            _linesArray = text.Split("\n").CleanLinesArray();
            ComplexityScore = _linesArray.Sum(CountLeadingWhitespaceIteration);
        }

        private static int CountLeadingWhitespaceIteration(string str)
        {
            // Equivalent to: return str.TakeWhile(char.IsWhiteSpace).Count();
            var length = str.Length;
            var count = 0;
            
            for (var i = 0; i < length; i++)
            {
                if (!char.IsWhiteSpace(str[i]))
                {
                    break;
                }

                count++;
            }

            return count;
        }
    }
}