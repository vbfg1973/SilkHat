using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Relationships;
using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Triples
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
            if (obj.GetType() != GetType()) return false;
            return Equals((IncludedInTriple)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NodeA, NodeB, Relationship);
        }
    }
}