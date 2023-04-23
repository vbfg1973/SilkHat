using System.Collections;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;
using CodeAnalysis.Domain.Extensions;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class SwitchCSharp : IEnumerable<object[]>
    {
        private const string FileName = "SwitchClass.CSharp";
        private const Language Language = Extensions.Language.CSharp;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "SwitchPattern", 0, Language };
            yield return new object[] { FileName, "SingleSwitch", 5, Language };
            yield return new object[] { FileName, "DoubleSwitch", 23, Language };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}