using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class HasComplexityRelationship : Relationship
    {
        public override string Type => "HAS_COMPLEXITY";
    }
}