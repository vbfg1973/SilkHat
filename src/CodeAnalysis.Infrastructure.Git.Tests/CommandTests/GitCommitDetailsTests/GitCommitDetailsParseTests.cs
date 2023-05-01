using CodeAnalysis.Infrastructure.Git.Commands;
using CodeAnalysis.Infrastructure.Git.Commands.Commits.CommitDetails;
using CodeAnalysis.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data;
using CodeAnalysis.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.Authors;
using CodeAnalysis.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.Dates;
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
        public void Given_GitLog_Number_Of_Commits_In_Log_Is_Correct(string fileName, int expectedCommitCount)
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
            var gitCommitDetails = FindGitCommitDetailsByShaId(fileName, shaId);
            
            var author = gitCommitDetails.Headers["Author"];

            author.Should().Contain(authorName);
            author.Should().Contain(authorEmail);
        }

        [Theory]
        [ClassData(typeof(DotnetSdkGitCommitDates))]
        [ClassData(typeof(LinuxGitCommitDates))]
        [ClassData(typeof(NopCommerceGitCommitDates))]
        [ClassData(typeof(RoslynAnalysersGitCommitDates))]
        public void Given_GitLog_Identified_By_ShaId_Date_Is_Correct(string fileName, string shaId, string dateString)
        {
            var gitCommitDetails = FindGitCommitDetailsByShaId(fileName, shaId);

            var commitDate = gitCommitDetails.Headers["Date"];

            commitDate
                .Should()
                .BeEquivalentTo(dateString);
        }
        
        private GitCommitDetails FindGitCommitDetailsByShaId(string fileName, string shaId)
        {
            var pathToLog = GetPathToLog(fileName);
            IProcessCommandRunner processCommandRunner = new FileReaderProcessRunner(pathToLog);
            var commandRunner = new GitCommitDetailsCommandRunner(processCommandRunner);

            return commandRunner.Run().First(x => string.Equals(x.Sha, shaId));
        }

        private string GetPathToLog(string fileName)
        {
            var newList = new List<string>(_dirPath) { fileName };
            return Path.Combine(newList.ToArray());
        }
    }
}