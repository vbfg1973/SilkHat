using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract
{
    public abstract class Triple
    {
        public abstract Node NodeA { get; }

        public abstract Node NodeB { get; }

        public abstract Relationship Relationship { get; }
    }
}