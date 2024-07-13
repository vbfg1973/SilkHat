using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class ImplementsRelationship : Relationship
    {
        public override string Type => "IMPLEMENTS";
    }
}