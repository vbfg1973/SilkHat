using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class BelongsToRelationship : Relationship
    {
        public override string Type => "BELONGS_TO";
    }
}