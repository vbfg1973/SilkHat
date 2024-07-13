using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples
{
    public class TripleInvocationOf : Triple
    {
        public TripleInvocationOf(
            InvocationNode invocationA,
            MethodNode methodNodeB)
            : base(invocationA, methodNodeB, new InvocationOfRelationship())
        {
        }
    }
}