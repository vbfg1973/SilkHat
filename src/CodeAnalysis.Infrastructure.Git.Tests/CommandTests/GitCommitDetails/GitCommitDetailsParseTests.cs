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
        [InlineData("linux.log", 283)]
        [InlineData("nopCommerce.log", 601)]
        [InlineData("dotnet-sdk.log", 935)]
        [InlineData("roslyn-analyzers.log", 789)]
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