﻿using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
{
    public class PackageNode : Node, IEquatable<PackageNode>
    {
        public PackageNode(string fullName, string name, string version)
            : base(fullName, name)
        {
            Version = version;
        }

        public override string Label { get; } = "Package";

        public string Version { get; }

        public bool Equals(PackageNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Label == other.Label && Version == other.Version;
        }

        public override string Set(string node)
        {
            return $"{base.Set(node)}, {node}.version = \"{Version}\"";
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PackageNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Version);
        }

        public static bool operator ==(PackageNode? left, PackageNode? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PackageNode? left, PackageNode? right)
        {
            return !Equals(left, right);
        }
    }
}