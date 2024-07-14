﻿using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples
{
    public class ConstructsTriple(MethodNode nodeA, ClassNode nodeB) : Triple, IEquatable<ConstructsTriple>
    {
        public override CodeNode NodeA { get; } = nodeA;
        public override TypeNode NodeB { get; } = nodeB;
        public override Relationship Relationship { get; } = new ConstructRelationship();

        public bool Equals(ConstructsTriple? other)
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
            return Equals((ConstructsTriple)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NodeA, NodeB, Relationship);
        }
    }
}