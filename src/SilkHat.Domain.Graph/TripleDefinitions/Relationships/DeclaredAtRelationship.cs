using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class DeclaredAtRelationship : Relationship
    {
        public override string Type => "DECLARED_AT";
    }
}