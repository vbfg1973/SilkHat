using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Relationships;
using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Triples
{
    public class InvokeTriple(MethodNode nodeA, InvocationNode nodeB) : Triple, IEquatable<InvokeTriple>
    {
        public override MethodNode NodeA { get; } = nodeA;
        public override InvocationNode NodeB { get; } = nodeB;
        public override Relationship Relationship { get; } = new InvokesRelationship();

        public bool Equals(InvokeTriple? other)
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
            return Equals((InvokeTriple)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NodeA, NodeB, Relationship);
        }

        public static bool operator ==(InvokeTriple? left, InvokeTriple? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InvokeTriple? left, InvokeTriple? right)
        {
            return !Equals(left, right);
        }
    }
}

