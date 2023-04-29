namespace CodeAnalysis.Infrastructure.Git.Models
{
    public enum LineChangeType
    {
        Add,
        Delete
    }

    public record GitFileComparison(GitChanges CurrentGit, GitChanges PreviousGit, string GitDiff);

    public record GitChanges(string ShaId, string Path, string Contents, LineChangeType LineChangeType,
        IList<int> LineNumbers);
}