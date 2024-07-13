using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples
{
    public class TripleImplementationOf : Triple
    {
        public TripleImplementationOf(
            MethodNode methodA,
            MethodNode methodNodeB)
            : base(methodA, methodNodeB, new ImplementsRelationship())
        {
        }
    }
}