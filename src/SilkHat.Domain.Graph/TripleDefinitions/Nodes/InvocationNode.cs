using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
{
    public class InvocationNode : Node, IEquatable<InvocationNode>
    {
        public InvocationNode(MethodNode parentMethodNode, MethodNode methodNode, int location) : base(
            $"{parentMethodNode.FullName}->{methodNode.FullName}",
            $"{parentMethodNode.FullName}->{methodNode.FullName}")
        {
            Location = location;
            Arguments = methodNode.Arguments;
            ReturnType = methodNode.ReturnType;
        }

        public InvocationNode() : base(string.Empty, string.Empty)
        {
        }

        public int Location { get; }

        private string Arguments { get; }

        private string ReturnType { get; }

        public override string Label { get; } = "Invocation";

        public override string Set(string node)
        {
            return $"{base.Set(node)}, {node}.location = \"{Location}\"";
        }

        public override int GetHashCode()
        {
           return HashCode.Combine(base.GetHashCode(), Location, Arguments, ReturnType);
        }

        public bool Equals(InvocationNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Location == other.Location && Arguments == other.Arguments && ReturnType == other.ReturnType && Label == other.Label;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((InvocationNode)obj);
        }

        public static bool operator ==(InvocationNode? left, InvocationNode? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InvocationNode? left, InvocationNode? right)
        {
            return !Equals(left, right);
        }
    }
}