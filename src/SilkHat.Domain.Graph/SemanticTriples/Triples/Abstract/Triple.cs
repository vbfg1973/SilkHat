using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract
{
    public abstract class Triple
    {
        public abstract Node NodeA { get; }

        public abstract Node NodeB { get; }

        public abstract Relationship Relationship { get; }
    }
}