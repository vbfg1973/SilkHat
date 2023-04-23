using System.Collections;
using CodeAnalysis.Domain.Analyzers.Complexity.Tests.Utilities;
using CodeAnalysis.Domain.Extensions;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class LambdaCSharp : IEnumerable<object[]>
    {
        private const string FileName = "LambdaClass.CSharp";
        private const Language Language = Extensions.Language.CSharp;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "SimpleLambda", 0, Language };
            yield return new object[] { FileName, "ParenthesizedLambda", 2, Language };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}