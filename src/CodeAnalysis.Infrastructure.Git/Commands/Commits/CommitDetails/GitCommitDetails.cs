namespace CodeAnalysis.Infrastructure.Git.Commands.Commits.CommitDetails
{
    public class GitCommitDetails
    {
        public GitCommitDetails()
        {
            Headers = new Dictionary<string, string>();
            Files = new List<GitFileStatus>();
            Message = string.Empty;
        }

        public Dictionary<string, string> Headers { get; set; }
        public string Sha { get; set; } = null!;
        public string Message { get; set; }
        public List<GitFileStatus> Files { get; set; }
    }
}