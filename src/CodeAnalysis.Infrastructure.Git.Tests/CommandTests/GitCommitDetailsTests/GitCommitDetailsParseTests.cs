using CodeAnalysis.Infrastructure.Git.Commands;
using CodeAnalysis.Infrastructure.Git.Commands.Commits.CommitDetails;
using CodeAnalysis.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.ClassData;
using CodeAnalysis.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.ClassData.AuthorData;
using CodeAnalysis.Infrastructure.Git.Tests.Helpers;
using FluentAssertions;

namespace CodeAnalysis.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests
{
    public class GitCommitDetailsParseTests
    {
        private readonly List<string> _dirPath = new()
        {
            "Resources",
            "GitLogOutput"
        };

        [Theory]
        [InlineData("linux.txt", 283)]
        [InlineData("nopCommerce.txt", 601)]
        [InlineData("dotnet-sdk.txt", 935)]
        [InlineData("roslyn-analyzers.txt", 789)]
        public void Given_GitLog_Number_Of_Commits_Is_Correct(string fileName, int expectedCommitCount)
        {
            var pathToLog = GetPathToLog(fileName);
            IProcessCommandRunner processCommandRunner = new FileReaderProcessRunner(pathToLog);
            var commandRunner = new GitCommitDetailsCommandRunner(processCommandRunner);

            commandRunner
                .Run()
                .Count()
                .Should()
                .Be(expectedCommitCount);
        }

        [Theory]
        [ClassData(typeof(DotnetSdkGitCommitAuthors))]
        [ClassData(typeof(LinuxGitCommitAuthors))]
        [ClassData(typeof(NopCommerceGitCommitAuthors))]
        [ClassData(typeof(RoslynAnalysersGitCommitAuthors))]
        public void Given_GitLog_Identified_By_ShaId_Author_Details_Are_Correct(string fileName, string shaId,
            string authorName,
            string authorEmail)
        {
            var pathToLog = GetPathToLog(fileName);
            IProcessCommandRunner processCommandRunner = new FileReaderProcessRunner(pathToLog);
            var commandRunner = new GitCommitDetailsCommandRunner(processCommandRunner);

            var gitCommitDetails = FindGitCommitDetailsByShaId(commandRunner, shaId);
            
            var author = gitCommitDetails.Headers["Author"];

            author.Should().Contain(authorName);
            author.Should().Contain(authorEmail);
        }

        private static GitCommitDetails FindGitCommitDetailsByShaId(GitCommitDetailsCommandRunner commandRunner, string shaId)
        {
            return commandRunner.Run().First(x => string.Equals(x.Sha, shaId));
        }

        private string GetPathToLog(string fileName)
        {
            var newList = new List<string>(_dirPath) { fileName };
            return Path.Combine(newList.ToArray());
        }
    }
}