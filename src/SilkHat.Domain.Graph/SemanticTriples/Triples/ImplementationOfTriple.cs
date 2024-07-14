using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Relationships;
using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Triples
{
    public class ImplementationOfTriple : Triple, IEquatable<ImplementationOfTriple>
    {
        public ImplementationOfTriple(MethodNode nodeA, MethodNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public ImplementationOfTriple(PropertyNode nodeA, PropertyNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public override CodeNode NodeA { get; }
        public override CodeNode NodeB { get; }
        public override Relationship Relationship { get; } = new ImplementsRelationship();

        public bool Equals(ImplementationOfTriple? other)
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
            return Equals((ImplementationOfTriple)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NodeA, NodeB, Relationship);
        }

        public static bool operator ==(ImplementationOfTriple? left, ImplementationOfTriple? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ImplementationOfTriple? left, ImplementationOfTriple? right)
        {
            return !Equals(left, right);
        }
    }
}