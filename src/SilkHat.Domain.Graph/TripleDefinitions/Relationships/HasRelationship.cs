using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class HasRelationship : Relationship
    {
        public override string Type => "HAS";
    }
}