using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
{
    public class PropertyNode : CodeNode, IEquatable<PropertyNode>
    {
        public PropertyNode(string fullName, string name, string returnType, string[] modifiers) : base(fullName, name,
            modifiers)
        {
            ReturnType = returnType;
        }

        public string ReturnType { get; }

        public override string Label { get; } = "Property";

        public bool Equals(PropertyNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && ReturnType == other.ReturnType && Label == other.Label;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropertyNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), ReturnType);
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