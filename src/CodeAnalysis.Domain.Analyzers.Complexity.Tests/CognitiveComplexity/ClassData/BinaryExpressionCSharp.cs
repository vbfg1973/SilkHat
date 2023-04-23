using System.Collections;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class BinaryExpressionCSharp : IEnumerable<object[]>
    {
        private const string FileName = "BinaryExpressionClass.CSharp";
        private const Language Language = Utilities.Language.CSharp;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "SimpleBinaryExpression", 1, Language };
            yield return new object[] { FileName, "NestedBinaryExpression", 1, Language };
            yield return new object[] { FileName, "DoublyNestedBinaryExpression", 2, Language };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}