using System.Collections;

namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.FileCount
{
    public class LinuxGitCommitFileCount : IEnumerable<object[]>
    {
        private const string FileName = "linux.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "7fa8a8ee9400fe8ec188426e40e481717bc5e924", 0 };
            yield return new object[] { FileName, "7b69ef62034492b816c65b1d15b45834df2b194e", 1 };
            yield return new object[] { FileName, "dc6b77badc752e4965919f7b81ad9e3725ae0a64", 11 };
            yield return new object[] { FileName, "2e2c6e9b72ce3d137984c867eb5625b8498cfe2b", 4 };
            yield return new object[] { FileName, "4774ad841bef97cc51df90195338c5b2573dd4cb", 1 };
            yield return new object[] { FileName, "700f11eb2cbea349bda2599b4b676b49d43b4175", 3 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}