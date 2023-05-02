namespace SilkHat.Infrastructure.Git.Commands.Commits.CommitParents
{
    public class GitCommitParents
    {
        public GitCommitParents()
        {
            Parents = new List<string>();
        }

        public string Sha { get; internal set; }
        public List<string> Parents { get; internal set; }
    }
}