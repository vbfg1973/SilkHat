using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class InvokedAtRelationship : Relationship
    {
        public override string Type => "INVOKED_AT";
    }
}