using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
{
    public class InvocationNode(MethodNode source, MethodNode target, int location) : Node, IEquatable<InvocationNode>
    {
        public MethodNode Source { get; } = source;
        public MethodNode Target { get; } = target;
        public override string Label => "Invocation";
        public override string FullName { get; } = $"{source.FullName} -> {target.FullName}";
        public override string Name { get; } = $"{source.Name} -> {target.Name}";

        public int Location { get; } = location;

        public bool Equals(InvocationNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FullName == other.FullName && 
                   Name == other.Name && 
                   Location == other.Location &&
                   Source == other.Source &&
                   Target == other.Target;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((InvocationNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullName, Name, Location, Source, Target);
        }
    }
}