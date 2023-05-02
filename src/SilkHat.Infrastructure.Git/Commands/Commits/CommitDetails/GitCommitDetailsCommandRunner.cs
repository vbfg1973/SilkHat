using System.Collections.Immutable;
using System.Text;
using SilkHat.Infrastructure.Git.Commands.Abstract;

namespace SilkHat.Infrastructure.Git.Commands.Commits.CommitDetails
{
    /// <summary>
    ///     Extract GitCommit objects from current branch of a repository
    /// </summary>
    public class GitCommitDetailsCommandRunner
    {
        private readonly IProcessCommandRunner _commandRunner;
        private bool _currentlyProcessingMessageBody;
        private StringBuilder _messageBuilder = new();

        public GitCommitDetailsCommandRunner(IProcessCommandRunner commandRunner)
        {
            _commandRunner = commandRunner;
            ResetMessageBodyProcessing();
        }

        /// <summary>
        ///     Extract GitCommitDetails from the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEnumerable<GitCommitDetails> Run(string path = ".")
        {
            var arguments = new CommitDetailsGitCommandLineArguments(path);
            path = GitCommitHelpers.CurrentWorkingDirectoryOrNominatedPath(path);

            GitCommitDetails? commit = null;
            foreach (var line in _commandRunner.Runner(arguments))
            {
                // Commit header - must go first for object initialisation
                if (line.TryParseCommitHeader(out var sha))
                {
                    if (commit != null)
                        yield return commit;

                    commit = new GitCommitDetails
                    {
                        Sha = sha
                    };
                }

                // Header lines - author, date, merge, etc
                if (line.TryParseHeader(out var headerName, out var headerValue))
                    commit?.Headers.Add(headerName, headerValue);

                // Commit messages
                if (line.IsMessageLine())
                    ProcessMessageLine(commit, line);

                else if (!line.IsMessageLine()) BuildGitCommitMessageIfFinishedProcessingMessageBody(commit);

                // File and changeKind
                if (line.TryParseFileStatusLine(out var changeKind, out var currentPath, out var oldPath))
                    commit?.Files.Add(new GitFileStatus(changeKind, currentPath, oldPath));
            }

            if (commit != null)
                yield return commit;
        }

        private void BuildGitCommitMessageIfFinishedProcessingMessageBody(GitCommitDetails? commit)
        {
            // If not currently processing then no body to collect
            if (!_currentlyProcessingMessageBody) return;

            // If commit is null then we haven't really started
            if (commit == null) return;

            commit.Message = _messageBuilder.ToString().NormaliseLineEndings();
            ResetMessageBodyProcessing();
        }

        private void ResetMessageBodyProcessing()
        {
            _messageBuilder = new StringBuilder();
            _currentlyProcessingMessageBody = false;
        }

        private void ProcessMessageLine(GitCommitDetails? commit, string messageLine)
        {
            if (commit == null) return;

            _currentlyProcessingMessageBody = true;
            _messageBuilder.AppendLine(messageLine);
        }

        private record CommitDetailsGitCommandLineArguments : AbstractGitCommandLineArguments
        {
            public CommitDetailsGitCommandLineArguments(string path)
            {
                Arguments = new List<string>
                {
                    Path.Combine($"--git-dir={path}", ".git"),
                    $"--work-tree={path}",
                    "log",
                    "--name-status"
                }.ToImmutableList();
            }
        }
    }
}