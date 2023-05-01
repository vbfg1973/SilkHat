using System.Collections;

namespace SilkHat.Domain.Analyzers.Complexity.Tests.IndentationComplexity.ClassData
{
    public class MixedAllWhiteSpaceStrings : IEnumerable<object[]>
    {
        private const string FileName = "BinaryExpressionClass.CSharp";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new string('\t', 5) }; // All tabs
            yield return new object[] { new string(' ', 5) }; // All spaces
            yield return new object[] { string.Concat(new string('\t', 5), new string(' ', 5)) }; // Tabs then spaces
            yield return new object[] { string.Concat(new string(' ', 5), new string('\t', 5)) }; // Spaces then tabs
            yield return new object[] { string.Concat(new string(' ', 5), new string('\t', 5), new string(' ', 5)) }; // Spaces, tabs, and spaces
            yield return new object[] { string.Concat(new string('\t', 5), new string(' ', 5), new string('\t', 5)) }; // Tabs, spaces and tabs
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}