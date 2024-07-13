using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples
{
    public class TripleOfType : Triple
    {
        public TripleOfType(
            ClassNode classA,
            TypeNode typeNodeB)
            : base(classA, typeNodeB, new OfTypeRelationship())
        {
        }

        public TripleOfType(
            RecordNode recordA,
            TypeNode typeNodeB)
            : base(recordA, typeNodeB, new OfTypeRelationship())
        {
        }

        public TripleOfType(
            InterfaceNode interfaceA,
            InterfaceNode interfaceNodeB)
            : base(interfaceA, interfaceNodeB, new OfTypeRelationship())
        {
        }
    }
}