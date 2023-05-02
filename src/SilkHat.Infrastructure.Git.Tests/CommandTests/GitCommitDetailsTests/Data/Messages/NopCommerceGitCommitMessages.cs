using System.Collections;

namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.Messages
{
    public class NopCommerceGitCommitMessages : IEnumerable<object[]>
    {
        private const string FileName = "nopCommerce.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "0b624d9f705d9023c5cb5539854d8460c647894e", 158 };
            yield return new object[] { FileName, "2871811eecf8f6666236df8bd1892b1ac9230824", 50 };
            yield return new object[] { FileName, "3b2d5ce2ceb67b1cc90d2b9900d19a0c42aebcca", 54 };
            yield return new object[] { FileName, "9e6aaa83bbded841dcec953fdf68ae839be3defe", 59 };
            yield return new object[] { FileName, "b0cd05bc68ece37e0ccb3f7a3e0fef376458356c", 44 };
            yield return new object[] { FileName, "ddf946b43c12040cb588a8610bc395a987185b5a", 36 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}