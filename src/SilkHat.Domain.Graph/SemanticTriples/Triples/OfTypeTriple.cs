using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Relationships;
using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Triples
{
    public class OfTypeTriple : Triple, IEquatable<OfTypeTriple>
    {
        public OfTypeTriple(ClassNode nodeA, TypeNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public OfTypeTriple(RecordNode nodeA, TypeNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public OfTypeTriple(InterfaceNode nodeA, TypeNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public override TypeNode NodeA { get; }
        public override TypeNode NodeB { get; }
        public override Relationship Relationship { get; } = new OfTypeRelationship();

        public bool Equals(OfTypeTriple? other)
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
            return Equals((OfTypeTriple)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NodeA, NodeB, Relationship);
        }
    }
}