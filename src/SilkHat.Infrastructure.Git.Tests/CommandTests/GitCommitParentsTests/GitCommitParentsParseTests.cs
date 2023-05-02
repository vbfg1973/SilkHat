using FluentAssertions;
using SilkHat.Infrastructure.Git.Commands;
using SilkHat.Infrastructure.Git.Commands.Commits.CommitDetails;
using SilkHat.Infrastructure.Git.Commands.Commits.CommitParents;
using SilkHat.Infrastructure.Git.Tests.Helpers;

namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitParentsTests
{
    public class GitCommitParentsParseTests : BaseCommandTests
    {
        public GitCommitParentsParseTests()
        {
            DirPath = new List<string>
            {
                "Resources",
                "ParentCommitsOutput"
            };
        }

        [Theory]
        [InlineData("8cb0c21cfae3c83ac02a5acb6edebefc2c2e0e77", 2)]
        public void Given_Sha_Correct_Number_Of_Parents_Is_Returned(string shaId, int expectedNumberOfParents)
        {
            var commitParents = GetGitCommitParents(shaId);

            // Valid length of parents list
            commitParents
                .Parents
                .Count
                .Should()
                .Be(expectedNumberOfParents);

            // Valid length of sha hash
            commitParents
                .Parents
                .All(x => x.Length == 40)
                .Should()
                .BeTrue();
        }
        
        private GitCommitParents GetGitCommitParents(string shaId)
        {
            var pathToFile = GetPathToTestResourceFile(shaId);
            
            IProcessCommandRunner processCommandRunner = new FileReaderProcessRunner(pathToFile);
            var commandRunner = new GitCommitParentsCommandRunner(processCommandRunner);

            return commandRunner.Run(shaId, string.Empty);
        }
    }
}