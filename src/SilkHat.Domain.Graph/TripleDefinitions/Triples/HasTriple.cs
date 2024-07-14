using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples
{
    public class HasTriple : Triple, IEquatable<HasTriple>
    {
        public HasTriple(TypeNode nodeA, MethodNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public HasTriple(TypeNode nodeA, PropertyNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public override TypeNode NodeA { get; }
        public override CodeNode NodeB { get; }
        public override Relationship Relationship { get; } = new HasRelationship();

        public bool Equals(HasTriple? other)
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
            return Equals((HasTriple)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NodeA, NodeB, Relationship);
        }
    }
}

