namespace CodeAnalysis.Infrastructure.Git.Models
{
    public class CommitFileSet : HashSet<CommitFile>, IEquatable<CommitFileSet>
    {
        public bool Equals(CommitFileSet? other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || this.OrderBy(x => x.Path).SequenceEqual(other.OrderBy(x => x.Path));
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CommitFileSet)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.Aggregate(19, (current, foo) => current * 31 + foo.GetHashCode());
            }
        }

        public static bool operator ==(CommitFileSet? left, CommitFileSet? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CommitFileSet? left, CommitFileSet? right)
        {
            return !Equals(left, right);
        }
    }

    public record CommitFile(string Path);

    public record CommitAuthor(string Name, string Email);

    public record CommitDetails(string ShaId, DateTimeOffset When, CommitAuthor Author, CommitFileSet Files);
}