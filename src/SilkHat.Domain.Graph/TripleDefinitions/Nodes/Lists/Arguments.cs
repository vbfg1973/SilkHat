namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes.Lists
{
    public class ArgumentList : List<Argument>, IEquatable<ArgumentList> 
    {
        public bool Equals(ArgumentList? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.SequenceEqual(other);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ArgumentList)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.Aggregate(19, (current, argument) => current * 31 + argument.GetHashCode());
            }
        }

        public static bool operator ==(ArgumentList? left, ArgumentList? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ArgumentList? left, ArgumentList? right)
        {
            return !Equals(left, right);
        }
    }
    
    public class Argument : IEquatable<Argument>
    {
        public Argument(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get; }
        public string Name { get; }

        public bool Equals(Argument? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type && Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Argument)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Name);
        }

        public static bool operator ==(Argument? left, Argument? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Argument? left, Argument? right)
        {
            return !Equals(left, right);
        }
    }
}