using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples
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
            if (obj.GetType() != this.GetType()) return false;
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