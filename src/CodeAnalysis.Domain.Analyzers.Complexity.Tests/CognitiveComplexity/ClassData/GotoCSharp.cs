using System.Collections;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class GotoCSharp : IEnumerable<object[]>
    {
        private const string FileName = "GotoClass.CSharp";
        private const Language Language = Utilities.Language.CSharp;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "GotoMethod", 1, Language };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}