namespace SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract
{
    public abstract class Node
    {
        public abstract string Label { get; }
        public abstract string FullName { get; }
        public abstract string Name { get; }

        protected string EmptyOrJoined(string[]? strings)
        {
            return strings == null
                ? ""
                : string.Join(", ", strings);
        }
    }
}