using System.Collections;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;
using CodeAnalysis.Domain.Extensions;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class ForLoopCSharp : IEnumerable<object[]>
    {
        private const string FileName = "ForClass.CSharp";
        private const Language Language = Extensions.Language.CSharp;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "SingleForLoop", 1, Language };
            yield return new object[] { FileName, "DoubleForLoop", 3, Language };
            yield return new object[] { FileName, "TripleForLoop", 6, Language };
            yield return new object[] { FileName, "QuadrupleForLoop", 10, Language };
            yield return new object[] { FileName, "QuintupleForLoop", 15, Language };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}