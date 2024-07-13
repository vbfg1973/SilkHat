using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples
{
    public class TripleInvokedAt : Triple
    {
        public TripleInvokedAt(
            InvocationNode invocationA,
            InvocationLocationNode invocationLocationNodeB)
            : base(invocationA, invocationLocationNodeB, new InvokedAtRelationship())
        {
        }
    }
}