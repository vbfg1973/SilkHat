using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
{
    public class InvocationLocationNode : Node, IEquatable<InvocationLocationNode>
    {
        public InvocationLocationNode(int location) : base(location.ToString("D10"), location.ToString("D10"))
        {
            Location = location;
        }

        public int Location { get; }

        public override string Label { get; } = "InvocationLocation";

        public bool Equals(InvocationLocationNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Location == other.Location && Label == other.Label;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((InvocationLocationNode)obj);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Location);
        }

        public static bool operator ==(InvocationLocationNode? left, InvocationLocationNode? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InvocationLocationNode? left, InvocationLocationNode? right)
        {
            return !Equals(left, right);
        }
    }
}