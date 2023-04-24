using System.Text.RegularExpressions;
using CodeAnalysis.Domain.Analyzers.Complexity.Abstract;

namespace CodeAnalysis.Domain.Analyzers.Complexity.IndentationComplexity
{
    public class IndentationComplexityAnalyzer : IIndentationComplexityAnalyzer
    {
        public int ComplexityScore { get; }
        
        
    }

    public class MeasureIndentation
    {
        private readonly string[] _linesArray;
        private readonly int[] _indentsArray;

        public MeasureIndentation(IEnumerable<string> lines)
        {
            _linesArray = lines.ToArray();
            _indentsArray = new int[_linesArray.Length];
            Measure();
        }

        private void Measure()
        {
            for (var i = 0; i < _linesArray.Length; i++)
            {
                _indentsArray[i] = CountLeadingWhitespaceIteration(_linesArray[i]);
            }
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