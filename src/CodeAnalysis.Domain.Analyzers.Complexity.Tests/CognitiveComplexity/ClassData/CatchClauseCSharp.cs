using System.Collections;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class CatchClauseCSharp : IEnumerable<object[]>
    {
        private const string FileName = "CatchClass.CSharp";
        private const Language Language = Utilities.Language.CSharp;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "SingleCatch", 1, Language };
            yield return new object[] { FileName, "DoubleCatch", 2, Language };
            yield return new object[] { FileName, "CatchWithFinally", 1, Language };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}