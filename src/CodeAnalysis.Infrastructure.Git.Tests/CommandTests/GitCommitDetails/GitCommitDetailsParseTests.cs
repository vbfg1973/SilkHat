using CodeAnalysis.Infrastructure.Git.Commands;
using CodeAnalysis.Infrastructure.Git.Commands.Commits.CommitDetails;
using CodeAnalysis.Infrastructure.Git.Tests.Helpers;
using FluentAssertions;

namespace CodeAnalysis.Infrastructure.Git.Tests.CommandTests.GitCommitDetails
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
        public void GivenGitLogReturnsCorrectNumberOfCommits(string fileName, int expectedCommitCount)
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

        private string GetPathToLog(string fileName)
        {
            var newList = new List<string>(_dirPath) { fileName };
            return Path.Combine(newList.ToArray());
        }
    }
}