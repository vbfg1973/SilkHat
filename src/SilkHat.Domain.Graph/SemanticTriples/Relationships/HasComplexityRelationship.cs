using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Relationships
{
    public class HasComplexityRelationship : Relationship, IEquatable<HasComplexityRelationship>
    {
        public override string Type => "HAS_COMPLEXITY";

        public bool Equals(HasComplexityRelationship? other)
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
            return Equals((HasComplexityRelationship)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type);
        }
    }
}