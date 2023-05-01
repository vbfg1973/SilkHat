using System.Collections;

namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.FileCount
{
    public class RoslynAnalysersGitCommitFileCount : IEnumerable<object[]>
    {
        private const string FileName = "roslyn-analyzers.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "178ece37489aa4ab4426000aba6c62ad0824e2c0", 0 };
            yield return new object[] { FileName, "38fa7f1501568eff9c5e52d9b572ca5d7666cd65", 17 };
            yield return new object[] { FileName, "bac0aeef523770c8ca917c8b12cb7937f9f871c4", 1 };
            yield return new object[] { FileName, "991cee4ba6efe0380543e00d1bb8151903c98b29", 2 };
            yield return new object[] { FileName, "87457b28b9dfce9deb1d804b5bf831d638ad9d3f", 5 };
            yield return new object[] { FileName, "ce66272b7ad48534441fe70c7fc0c710ba8a9927", 2 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}