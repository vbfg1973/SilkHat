using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Relationships
{
    public class DeclaredAtRelationship : Relationship, IEquatable<DeclaredAtRelationship>
    {
        public override string Type => "DECLARED_AT";

        public bool Equals(DeclaredAtRelationship? other)
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
            return Equals((DeclaredAtRelationship)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type);
        }
    }
}