﻿using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Nodes
{
    public class FileNode(string fullName, string name) : Node, IEquatable<FileNode>
    {
        public override string Label => "File";
        public override string FullName => fullName;
        public override string Name => name;

        public bool Equals(FileNode? other)
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
            return Equals((FileNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Label, FullName, Name);
        }
    }
}