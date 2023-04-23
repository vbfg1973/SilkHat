using System.Collections;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class BinaryExpressionVisualBasic : IEnumerable<object[]>
    {
        private const string FileName = "BinaryExpressionClass.VisualBasic";
        private const Language Language = Utilities.Language.VisualBasic;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "SimpleBinaryExpression", 1, Language };
            yield return new object[] { FileName, "NestedBinaryExpression", 2, Language };
            yield return new object[] { FileName, "DoublyNestedBinaryExpression", 3, Language };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}