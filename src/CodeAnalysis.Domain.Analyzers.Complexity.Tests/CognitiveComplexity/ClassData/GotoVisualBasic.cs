using System.Collections;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class GotoVisualBasic : IEnumerable<object[]>
    {
        private const string FileName = "GotoClass.VisualBasic";
        private const Language Language = Utilities.Language.VisualBasic;

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