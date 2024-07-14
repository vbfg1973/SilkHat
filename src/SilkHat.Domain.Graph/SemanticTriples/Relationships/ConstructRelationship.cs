using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Relationships
{
    public class ConstructRelationship : Relationship, IEquatable<ConstructRelationship>
    {
        public override string Type => "CONSTRUCT";

        public bool Equals(ConstructRelationship? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ConstructRelationship)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type);
        }
    }
}