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
        public void Given_FileStatus_Line_Is_True(string line)
        {
            line
                .IsFileStatusLine()
                .Should()
                .BeTrue();
        }

        [Theory]
        [ClassData(typeof(AllExceptFileStatusLinesClassData))]
        public void Given_FileStatus_Line_Is_False(string line)
        {
            line
                .IsFileStatusLine()
                .Should()
                .BeFalse();
        }

        #endregion

        #region Header Lines

        [Theory]
        [ClassData(typeof(HeaderLinesClassData))]
        public void Given_Header_Line_Is_True(string line)
        {
            line.IsHeader()
                .Should()
                .BeTrue();
        }

        [Theory]
        [ClassData(typeof(AllExceptHeaderLinesClassData))]
        public void Given_Header_Line_Is_False(string line)
        {
            line.IsHeader()
                .Should()
                .BeFalse();
        }

        #endregion

        #region Commit Lines

        [Theory]
        [ClassData(typeof(CommitLinesClassData))]
        public void Given_Commit_Line_Is_True(string line)
        {
            line.IsCommitHeader()
                .Should()
                .BeTrue();
        }

        [Theory]
        [ClassData(typeof(AllExceptCommitLinesClassData))]
        public void Given_Commit_Line_Is_False(string line)
        {
            line.IsCommitHeader()
                .Should()
                .BeFalse();
        }

        #endregion

        #region Message Body Lines

        [Theory]
        [ClassData(typeof(MessageBodyLinesClassData))]
        public void Given_MessageBody_Line_Is_True(string line)
        {
            line.IsMessageLine()
                .Should()
                .BeTrue();
        }

        [Theory]
        [ClassData(typeof(AllExceptMessageBodyLinesClassData))]
        public void Given_MessageBody_Line_Is_False(string line)
        {
            line.IsMessageLine()
                .Should()
                .BeFalse();
        }

        #endregion
    }
}