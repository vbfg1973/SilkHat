using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Lists;

namespace SilkHat.Domain.Graph.SemanticTriples.Nodes
{
    public class MethodNode : CodeNode, IEquatable<MethodNode>
    {
        public MethodNode(string fullName, string name, (string name, string type)[] args, string returnType,
            string[]? modifiers = null)
        {
            FullName = fullName;
            Name = name;
            Args = new ArgumentList();
            ReturnType = returnType;
            Modifiers = EmptyOrJoined(modifiers);

            Args.AddRange(args.Select(x => new Argument(x.name, x.type)));
        }

        public override string Label => "Method";
        public override string FullName { get; }
        public override string Name { get; }
        public ArgumentList Args { get; }
        public string ReturnType { get; }
        public override string Modifiers { get; }

        public bool Equals(MethodNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Label == other.Label &&
                   FullName == other.FullName &&
                   Name == other.Name &&
                   Args.Equals(other.Args) &&
                   ReturnType == other.ReturnType &&
                   Modifiers == other.Modifiers;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MethodNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Label, FullName, Name, Args, ReturnType, Modifiers);
        }

        public static bool operator ==(MethodNode? left, MethodNode? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MethodNode? left, MethodNode? right)
        {
            return !Equals(left, right);
        }
    }
}