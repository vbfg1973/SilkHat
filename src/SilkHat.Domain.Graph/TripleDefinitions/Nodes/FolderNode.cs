﻿using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
{
    public class FolderNode(string fullName, string name) : Node, IEquatable<FolderNode>
    {
        public override string Label { get; } = "Folder";
        public override string FullName { get; } = fullName;
        public override string Name { get; } = name;

        public bool Equals(FolderNode? other)
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
            return Equals((FolderNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Label, FullName, Name);
        }
    }
}