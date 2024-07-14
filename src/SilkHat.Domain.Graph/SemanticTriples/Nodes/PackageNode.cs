using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Nodes
{
    public class PackageNode(string fullName, string name, string version) : Node, IEquatable<PackageNode>
    {
        public override string Label => "Package";
        public override string FullName { get; } = fullName;
        public override string Name { get; } = name;
        public string Version { get; } = version;

        public bool Equals(PackageNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FullName == other.FullName && Name == other.Name && Version == other.Version;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PackageNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullName, Name, Version);
        }
    }
}