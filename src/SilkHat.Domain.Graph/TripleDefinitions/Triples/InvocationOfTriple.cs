using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples
{
    public class InvocationOfTriple : Triple, IEquatable<InvocationOfTriple>
    {
        public InvocationOfTriple(InvocationNode nodeA, MethodNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }
        
        public InvocationOfTriple(InvocationNode nodeA, PropertyNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }

        public override InvocationNode NodeA { get; }
        public override CodeNode NodeB { get; }
        public override Relationship Relationship { get; } = new InvocationOfRelationship();

        public bool Equals(InvocationOfTriple? other)
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
            return Equals((InvocationOfTriple)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NodeA, NodeB, Relationship);
        }
    }
}

