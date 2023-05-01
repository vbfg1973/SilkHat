using System.Collections;
using SilkHat.Domain.Extensions;

namespace SilkHat.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class GotoVisualBasic : IEnumerable<object[]>
    {
        private const string FileName = "GotoClass.VisualBasic";
        private const Language Language = Extensions.Language.VisualBasic;

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