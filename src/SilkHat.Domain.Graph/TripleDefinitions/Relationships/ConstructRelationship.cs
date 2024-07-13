using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class ConstructRelationship : Relationship
    {
        public override string Type => "CONSTRUCT";
    }
}