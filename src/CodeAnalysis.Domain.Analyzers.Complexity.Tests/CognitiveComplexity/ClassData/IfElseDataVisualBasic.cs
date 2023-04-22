using System.Collections;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class IfElseDataVisualBasic : IEnumerable<object[]>
    {
        private const string FileName = "VisualBasicIfElseClass.VisualBasic";
        private const Language Language = Utilities.Language.VisualBasic;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "BasicMethod", 0, Language };
            yield return new object[] { FileName, "Method_CoalescedIfElse", 0, Language };
            yield return new object[] { FileName, "Method_IfStatement", 1, Language };
            yield return new object[] { FileName, "Method_IfElseStatement", 2, Language };
            yield return new object[] { FileName, "Method_IfElseIfStatement", 3, Language };
            yield return new object[] { FileName, "Method_NestedIfElseStatement", 5, Language };
            yield return new object[] { FileName, "Method_DoublyNestedIfElseStatement", 8, Language };
            yield return new object[] { FileName, "Method_DeeplyNestedIfElseStatement", 12, Language };
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}