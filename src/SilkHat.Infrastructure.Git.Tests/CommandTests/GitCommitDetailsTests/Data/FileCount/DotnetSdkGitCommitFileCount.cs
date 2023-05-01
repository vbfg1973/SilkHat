using System.Collections;

namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.FileCount
{
    public class DotnetSdkGitCommitFileCount : IEnumerable<object[]>
    {
        private const string FileName = "dotnet-sdk.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "ff634058dca93dd80a0b23f06fc3c64d419e16e8", 2 };
            yield return new object[] { FileName, "19abc283c3e13dabaf94407e986c1d513d957c8d", 14 };
            yield return new object[] { FileName, "e006ecf6a4171974ae31bd91ebedcd27c5ca0864", 1 };
            yield return new object[] { FileName, "f2f4a3ecf1cec94cb4c32c16e1c85d25c66646a2", 0 };
            yield return new object[] { FileName, "cf8c22ef8be20451ea0e2c8ef5b0f94b10eea2db", 0 };
            yield return new object[] { FileName, "0091fb4deb0134b885c67a93f4ca3ceaaecf4d21", 0 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}