using BenchmarkDotNet.Attributes;

namespace SilkHat.Utilities.Benchmarking.Complexity.Indentation
{
    [MemoryDiagnoser]
    public class IndentationBenchmarks
    {
        private readonly string[] strings;

        public IndentationBenchmarks()
        {
            strings = BuildStrings().ToArray();
        }

        private IEnumerable<string> BuildStrings()
        {
            for (var i = 0; i <= 100; i += 10)
            {
                yield return string.Concat(new string(' ', i), "Some text");
                yield return string.Concat(new string('\t', i), "Some text");
            }
        }


        [Benchmark(Baseline = true)]
        public void LeadingWhiteSpaceLinq()
        {
            for (var i = 0; i < strings.Length; i++)
            {
                var count = strings[i].TakeWhile(char.IsWhiteSpace).Count();
            }
        }

        [Benchmark]
        public void LeadingWhitespaceIteration()
        {
            for (var i = 0; i < strings.Length; i++)
            {
            }
        }
    }
}