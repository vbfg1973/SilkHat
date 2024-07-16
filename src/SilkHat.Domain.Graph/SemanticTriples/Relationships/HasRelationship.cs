using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Relationships
{
    public class HasRelationship : Relationship, IEquatable<HasRelationship>
    {
        public override string Type => "HAS";

        public bool Equals(HasRelationship? other)
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
            return Equals((HasRelationship)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type);
        }
    }
    
    public class HasLocationRelationship : Relationship, IEquatable<HasLocationRelationship>
    {
        public override string Type => "HAS_LOCATION";

        public bool Equals(HasLocationRelationship? other)
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
            return Equals((HasLocationRelationship)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type);
        }
    }
}