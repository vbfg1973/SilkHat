using System.Collections;

namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.Authors
{
    public class LinuxGitCommitAuthors : IEnumerable<object[]>
    {
        private const string FileName = "linux.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                FileName, "7fa8a8ee9400fe8ec188426e40e481717bc5e924", "Linus Torvalds", "torvalds@linux-foundation.org"
            };
            yield return new object[]
            {
                FileName, "7b69ef62034492b816c65b1d15b45834df2b194e", "Uwe Kleine-König",
                "u.kleine-koenig@pengutronix.de"
            };
            yield return new object[]
                { FileName, "e644b2f498d297a928efcb7ff6f900c27f8b788e", "Lino Sanfilippo", "l.sanfilippo@kunbus.com" };
            yield return new object[]
                { FileName, "c3a6ef330a08eba406f82b0b8cbca4e4d9b7c4ba", "Jiri Kosina", "jkosina@suse.cz" };
            yield return new object[]
            {
                FileName, "d3f2c402e44887e507b65d65f0d0515d46575bf5", "Rafael J. Wysocki", "rafael.j.wysocki@intel.com"
            };
            yield return new object[]
                { FileName, "ee3392ed16b064594a14ce5886e412efb05ed17b", "Jakub Kicinski", "kuba@kernel.org" };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}