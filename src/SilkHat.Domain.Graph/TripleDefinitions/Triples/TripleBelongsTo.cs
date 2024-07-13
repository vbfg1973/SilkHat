using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples
{
    public class TripleBelongsTo : Triple
    {
        public TripleBelongsTo(
            TypeNode typeA,
            ProjectNode projectNodeB)
            : base(typeA, projectNodeB, new BelongsToRelationship())
        {
        }
    }
}