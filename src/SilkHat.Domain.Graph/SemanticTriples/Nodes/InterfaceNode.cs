using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Nodes
{
    public class InterfaceNode : TypeNode, IEquatable<InterfaceNode>
    {
        public InterfaceNode(string fullName, string name, string[]? modifiers = null)
        {
            FullName = fullName;
            Name = name;
            Modifiers = EmptyOrJoined(modifiers);
        }

        public override string Label =>  "Interface";
        public override string FullName { get; }
        public override string Name { get; }
        public override string Modifiers { get; }

        public bool Equals(InterfaceNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Label == other.Label && FullName == other.FullName && Name == other.Name && Modifiers == other.Modifiers;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((InterfaceNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Label, FullName, Name, Modifiers);
        }
    }
}

