using System.Collections;
using SilkHat.Domain.Extensions;

namespace SilkHat.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
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