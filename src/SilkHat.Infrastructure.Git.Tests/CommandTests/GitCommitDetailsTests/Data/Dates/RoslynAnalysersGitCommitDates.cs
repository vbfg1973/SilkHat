using System.Collections;

namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.Dates
{
    public class RoslynAnalysersGitCommitDates : IEnumerable<object[]>
    {
        private const string FileName = "roslyn-analyzers.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
                { FileName, "178ece37489aa4ab4426000aba6c62ad0824e2c0", "Mon Apr 24 09:29:32 2023 +0530" };
            yield return new object[]
                { FileName, "38fa7f1501568eff9c5e52d9b572ca5d7666cd65", "Sun Feb 19 15:43:37 2023 +0200" };
            yield return new object[]
                { FileName, "bac0aeef523770c8ca917c8b12cb7937f9f871c4", "Tue Apr 4 15:29:11 2023 -0700" };
            yield return new object[]
                { FileName, "991cee4ba6efe0380543e00d1bb8151903c98b29", "Fri Feb 3 15:35:53 2023 +0100" };
            yield return new object[]
                { FileName, "87457b28b9dfce9deb1d804b5bf831d638ad9d3f", "Wed Jan 25 19:25:16 2023 -0800" };
            yield return new object[]
                { FileName, "ce66272b7ad48534441fe70c7fc0c710ba8a9927", "Thu Feb 2 15:59:34 2023 -0500" };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}