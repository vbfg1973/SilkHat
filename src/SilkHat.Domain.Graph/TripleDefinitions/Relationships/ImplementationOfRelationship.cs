using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class ImplementationOfRelationship : Relationship
    {
        public override string Type => "IMPLEMENTS";
    }
}