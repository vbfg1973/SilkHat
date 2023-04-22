using System.Collections;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class SwitchCSharp : IEnumerable<object[]>
    {
        private const string FileName = "SwitchClass.CSharp";
        private const Language Language = Utilities.Language.CSharp;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "SingleSwitch", 5, Language };
            yield return new object[] { FileName, "DoubleSwitch", 23, Language };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}