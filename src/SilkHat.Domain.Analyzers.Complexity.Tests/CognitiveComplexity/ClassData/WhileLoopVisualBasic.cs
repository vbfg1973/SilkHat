﻿using System.Collections;
using SilkHat.Domain.Extensions;

namespace SilkHat.Domain.Analyzers.Complexity.Tests.CognitiveComplexity.ClassData
{
    public class WhileLoopVisualBasic : IEnumerable<object[]>
    {
        private const string FileName = "WhileClass.VisualBasic";
        private const Language Language = Extensions.Language.VisualBasic;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "SingleWhileLoop", 1, Language };
            yield return new object[] { FileName, "DoubleWhileLoop", 3, Language };
            yield return new object[] { FileName, "TripleWhileLoop", 6, Language };
            yield return new object[] { FileName, "QuadrupleWhileLoop", 10, Language };
            yield return new object[] { FileName, "QuintupleWhileLoop", 15, Language };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}