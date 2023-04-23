using System.Collections;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;
using CodeAnalysis.Domain.Extensions;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class MethodVisualBasic : IEnumerable<object[]>
    {
        private const string FileName = "MethodClass.VisualBasic";
        private const Language Language = Extensions.Language.VisualBasic;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "SimpleMethod", 0, Language };
            yield return new object[] { FileName, "MethodWithInvocation", 0, Language };
            yield return new object[] { FileName, "MethodWithRecursiveInvocation", 1, Language };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}