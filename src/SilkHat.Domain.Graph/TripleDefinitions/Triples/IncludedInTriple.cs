using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples
{
    public class IncludedInTriple : Triple, IEquatable<IncludedInTriple>
    {
        public IncludedInTriple(ProjectNode nodeA, FolderNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public IncludedInTriple(FolderNode nodeA, FolderNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public IncludedInTriple(FileNode nodeA, FolderNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public override Node NodeA { get; }
        public override Node NodeB { get; }
        public override Relationship Relationship { get; } = new IncludedInRelationship();

        public bool Equals(IncludedInTriple? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return NodeA.Equals(other.NodeA) && NodeB.Equals(other.NodeB) && Relationship.Equals(other.Relationship);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IncludedInTriple)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NodeA, NodeB, Relationship);
        }
    }
}

