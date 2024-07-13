using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class DependsOnRelationship : Relationship
    {
        public override string Type => "DEPENDS_ON";
    }
}