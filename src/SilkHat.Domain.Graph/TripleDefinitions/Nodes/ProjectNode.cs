﻿using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
{
    public class ProjectNode : Node, IEquatable<ProjectNode>
    {
        public ProjectNode(string name)
            : this(name, name)
        {
        }

        public ProjectNode(string fullName, string name)
            : base(fullName, name)
        {
        }

        public ProjectNode() : base(string.Empty, string.Empty)
        {
        }

        public override string Label { get; } = "Project";

        public bool Equals(ProjectNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Label == other.Label;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ProjectNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Label);
        }

        public static bool operator ==(ProjectNode? left, ProjectNode? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProjectNode? left, ProjectNode? right)
        {
            return !Equals(left, right);
        }
    }
}