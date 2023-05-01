using System.Collections;

namespace CodeAnalysis.Infrastructure.Git.Tests.CommandTests.GitCommitDetails.ClassData
{
    public class LinuxGitCommitLogCount : IEnumerable<object[]>
    {
        private readonly List<string> PathElements = new() { "Resource", "CommitOutput", "linux.gitcommit.log" };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { Path.Combine(PathElements.ToArray()), 284 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}