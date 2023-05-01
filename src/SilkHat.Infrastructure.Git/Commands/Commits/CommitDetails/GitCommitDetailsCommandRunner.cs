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

        public IEnumerable<GitCommitDetails> Run(string path = "")
        {
            var arguments = new GitCommitDetailsArguments(path);
            path = GitCommitHelpers.CurrentWorkingDirectoryOrNominatedPath(path);

            GitCommitDetails? commit = null;
            foreach (var line in _commandRunner.Runner(arguments))
            {
                // Commit header - must go first for object initialisation
                if (line.IsCommitHeader())
                {
                    if (commit != null)
                        yield return commit;

                    commit = ProcessCommitLine(line);
                }

                // Header lines - author, date, merge, etc
                if (line.IsHeader()) ProcessHeaderLine(commit, line);

                // Commit messages
                if (line.IsMessageLine())
                    ProcessMessageLine(commit, line);

                else if (!line.IsMessageLine()) BuildGitCommitMessageIfFinishedProcessingMessageBody(commit);

                // File and changeKind
                if (line.IsFileStatusLine())
                    // file status
                    ExtractFileStatusLine(commit, line);
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

        private static GitCommitDetails ProcessCommitLine(string commitLine)
        {
            return new GitCommitDetails
            {
                Sha = commitLine.Split(' ')[1].Trim()
            };
        }

        private static void ProcessHeaderLine(GitCommitDetails? gitCommitDetails, string headerLine)
        {
            var elements = headerLine.Split(':');
            gitCommitDetails?.Headers.Add(elements[0], string.Join(':', elements.Skip(1)).Trim());
        }

        private void ProcessMessageLine(GitCommitDetails? commit, string messageLine)
        {
            if (commit == null) return;

            _currentlyProcessingMessageBody = true;
            _messageBuilder.AppendLine(messageLine);
        }

        private static void ExtractFileStatusLine(GitCommitDetails? gitCommitDetails, string fileStatusLine)
        {
            var statusElements = fileStatusLine.Split('\t');
            gitCommitDetails!.Files.Add(new GitFileStatus
                { Status = statusElements[0].Trim(), File = statusElements[1].Trim() });
        }

        private record GitCommitDetailsArguments : AbstractGitArgument
        {
            public GitCommitDetailsArguments(string path)
            {
                Arguments = new List<string>
                {
                    Path.Combine($"--git-dir={path}", ".git"),
                    $"--work-tree={path}",
                    "log",
                    "--name-status"
                };
            }
        }
    }
}