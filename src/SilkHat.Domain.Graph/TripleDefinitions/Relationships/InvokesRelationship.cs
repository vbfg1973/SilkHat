using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class InvokesRelationship : Relationship
    {
        public override string Type => "INVOKES";
    }
}