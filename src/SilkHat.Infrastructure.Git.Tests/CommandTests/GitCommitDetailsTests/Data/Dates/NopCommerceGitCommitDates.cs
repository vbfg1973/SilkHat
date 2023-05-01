using System.Collections;

namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.Dates
{
    public class NopCommerceGitCommitDates : IEnumerable<object[]>
    {
        private const string FileName = "nopCommerce.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
                { FileName, "0b624d9f705d9023c5cb5539854d8460c647894e", "Fri Apr 28 15:55:00 2023 +0300" };
            yield return new object[]
                { FileName, "9e6aaa83bbded841dcec953fdf68ae839be3defe", "Thu Mar 16 17:21:05 2023 +0700" };
            yield return new object[]
                { FileName, "b0cd05bc68ece37e0ccb3f7a3e0fef376458356c", "Fri Mar 10 14:29:56 2023 +0300" };
            yield return new object[]
                { FileName, "2871811eecf8f6666236df8bd1892b1ac9230824", "Mon Apr 24 10:00:47 2023 +0300" };
            yield return new object[]
                { FileName, "ddf946b43c12040cb588a8610bc395a987185b5a", "Wed Jan 18 14:20:41 2023 +0100" };
            yield return new object[]
                { FileName, "3b2d5ce2ceb67b1cc90d2b9900d19a0c42aebcca", "Tue May 24 10:36:58 2022 +0200" };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}