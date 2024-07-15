using SilkHat.Domain.Common.Locations;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples
{
    public class LocationNode : Node, IEquatable<LocationNode>
    {
        public LocationNode(string filePath, Location location)
        {
            FullName = filePath;
            Name = filePath;
            Location = location;
        }

        public override string Label => "Location";
        public override string FullName { get; }
        public override string Name { get; }
        public Location Location { get; }

        public bool Equals(LocationNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FullName == other.FullName && Name == other.Name && Location.Equals(other.Location);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LocationNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullName, Name, Location);
        }

        public static bool operator ==(LocationNode? left, LocationNode? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LocationNode? left, LocationNode? right)
        {
            return !Equals(left, right);
        }
    }
}