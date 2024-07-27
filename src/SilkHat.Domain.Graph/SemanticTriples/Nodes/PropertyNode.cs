using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Nodes
{
    public class PropertyNode : MemberNode, IEquatable<PropertyNode>
    {
        public PropertyNode(string fullName, string name, string returnType, string[]? modifiers = null)
        {
            FullName = fullName;
            Name = name;
            ReturnType = returnType;
            Modifiers = EmptyOrJoined(modifiers);
        }

        public override string Label => "Property";
        public override string FullName { get; }
        public override string Name { get; }
        public string ReturnType { get; }
        public override string Modifiers { get; }

        public bool Equals(PropertyNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Label == other.Label &&
                   FullName == other.FullName &&
                   Name == other.Name &&
                   ReturnType == other.ReturnType &&
                   Modifiers == other.Modifiers;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PropertyNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Label, FullName, Name, ReturnType, Modifiers);
        }

        public static bool operator ==(PropertyNode? left, PropertyNode? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PropertyNode? left, PropertyNode? right)
        {
            return !Equals(left, right);
        }
    }
}