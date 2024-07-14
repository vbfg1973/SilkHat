using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Relationships;
using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Triples
{
    public class HasComplexityTriple(MethodNode nodeA, CognitiveComplexityNode nodeB)
        : Triple, IEquatable<HasComplexityTriple>
    {
        public override Node NodeA { get; } = nodeA;
        public override Node NodeB { get; } = nodeB;
        public override Relationship Relationship { get; } = new HasComplexityRelationship();

        public bool Equals(HasComplexityTriple? other)
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
            return Equals((HasComplexityTriple)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NodeA, NodeB, Relationship);
        }
    }
}