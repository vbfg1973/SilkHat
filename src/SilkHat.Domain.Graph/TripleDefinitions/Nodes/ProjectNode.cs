using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
{
    public class ProjectNode : Node, IEquatable<ProjectNode>
    {
        public ProjectNode(string name)
        {
            FullName = name;
            Name = name;
        }
        
        public ProjectNode(string fullName, string name)
        {
            FullName = fullName;
            Name = name;
        }
        
        public override string Label =>  "Project";
        public override string FullName { get; }
        public override string Name { get; }

        public bool Equals(ProjectNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FullName == other.FullName && Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProjectNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullName, Name);
        }
    }
}

