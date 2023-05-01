using System.Collections;

namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.Dates
{
    public class LinuxGitCommitDates : IEnumerable<object[]>
    {
        private const string FileName = "linux.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
                { FileName, "7fa8a8ee9400fe8ec188426e40e481717bc5e924", "Thu Apr 27 19:42:02 2023 -0700" };
            yield return new object[]
                { FileName, "7b69ef62034492b816c65b1d15b45834df2b194e", "Mon Mar 20 09:06:07 2023 +0100" };
            yield return new object[]
                { FileName, "e644b2f498d297a928efcb7ff6f900c27f8b788e", "Thu Nov 24 14:55:38 2022 +0100" };
            yield return new object[]
                { FileName, "c3a6ef330a08eba406f82b0b8cbca4e4d9b7c4ba", "Wed Apr 26 23:09:07 2023 +0200" };
            yield return new object[]
                { FileName, "d3f2c402e44887e507b65d65f0d0515d46575bf5", "Mon Apr 24 18:37:20 2023 +0200" };
            yield return new object[]
                { FileName, "ee3392ed16b064594a14ce5886e412efb05ed17b", "Mon Apr 24 18:45:11 2023 -0700" };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}