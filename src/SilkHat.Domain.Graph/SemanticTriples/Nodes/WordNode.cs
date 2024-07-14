using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Nodes
{
    public class WordNode : Node, IEquatable<WordNode>
    {
        public WordNode(string fullName, string name)
        {
            FullName = fullName;
            Name = name;
        }

        public override string Label => "Word";
        public override string FullName { get; }
        public override string Name { get; }

        public bool Equals(WordNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Label == other.Label &&
                   FullName == other.FullName &&
                   Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WordNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Label, FullName, Name);
        }
    }
}