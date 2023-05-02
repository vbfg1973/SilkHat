using FluentAssertions;
using SilkHat.Infrastructure.Git.Commands.Commits;
using SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitHelperTests.Data;

namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitHelperTests
{
    public class GitCommitHelperTests
    {
        #region File Status Lines

        [Theory]
        [ClassData(typeof(FileStatusLinesClassData))]
        public void Given_A_FileStatus_Line_Matches(string line)
        {
            line
                .TryParseFileStatusLine(out var changeKind, out var currentPath, out var oldPath)
                .Should()
                .BeTrue();
        }

        [Theory]
        [ClassData(typeof(AllExceptFileStatusLinesClassData))]
        public void Given_A_Non_FileStatus_Line_Does_Not_Match(string line)
        {
            line
                .TryParseFileStatusLine(out var changeKind, out var currentPath, out var oldPath)
                .Should()
                .BeFalse();
        }

        #endregion

        #region Header Lines

        [Theory]
        [ClassData(typeof(HeaderLinesClassData))]
        public void Given_A_Header_Line_Matches(string line)
        {
            line.TryParseHeader(out var headerName, out var headerValue)
                .Should()
                .BeTrue();
        }

        [Theory]
        [ClassData(typeof(AllExceptHeaderLinesClassData))]
        public void Given_A_Non_Header_Line_Does_Not_Match(string line)
        {
            line.TryParseHeader(out var headerName, out var headerValue)
                .Should()
                .BeFalse();
        }

        #endregion

        #region Commit Lines

        [Theory]
        [ClassData(typeof(CommitLinesClassData))]
        public void Given_A_Commit_Line_Matches(string line)
        {
            line.TryParseCommitHeader(out var sha)
                .Should()
                .BeTrue();
        }

        [Theory]
        [ClassData(typeof(AllExceptCommitLinesClassData))]
        public void Given_A_Non_Commit_Line_Does_Not_Match(string line)
        {
            line.TryParseCommitHeader(out var sha)
                .Should()
                .BeFalse();
        }

        #endregion

        #region Message Body Lines

        [Theory]
        [ClassData(typeof(MessageBodyLinesClassData))]
        public void Given_A_MessageBody_Line_Matches(string line)
        {
            line.IsMessageLine()
                .Should()
                .BeTrue();
        }

        [Theory]
        [ClassData(typeof(AllExceptMessageBodyLinesClassData))]
        public void Given_A_Non_MessageBody_Line_Does_Not_Match(string line)
        {
            line.IsMessageLine()
                .Should()
                .BeFalse();
        }

        #endregion
    }
}