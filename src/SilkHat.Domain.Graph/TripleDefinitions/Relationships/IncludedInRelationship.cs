using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class IncludedInRelationship : Relationship
    {
        public override string Type => "INCLUDED_IN";
    }
}