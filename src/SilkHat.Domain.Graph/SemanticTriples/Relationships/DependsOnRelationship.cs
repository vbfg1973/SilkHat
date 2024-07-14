using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Relationships
{
    public class DependsOnRelationship : Relationship, IEquatable<DependsOnRelationship>
    {
        public override string Type => "DEPENDS_ON";

        public bool Equals(DependsOnRelationship? other)
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
            return Equals((DependsOnRelationship)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type);
        }
    }
}