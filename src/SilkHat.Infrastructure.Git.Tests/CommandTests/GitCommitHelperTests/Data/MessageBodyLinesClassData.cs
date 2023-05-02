using System.Collections;
using SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitHelperTests.Data.Abstract;

namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitHelperTests.Data
{
    public class MessageBodyLinesClassData : BaseGitCommitHelperClassData, IEnumerable<object[]>
    {
        private const string FileName = "MessageBodyLines.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var line in ReadFile(FileName))
            {
                yield return new object[] { line };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}