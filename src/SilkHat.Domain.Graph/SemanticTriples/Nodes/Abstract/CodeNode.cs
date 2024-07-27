namespace SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract
{
    public abstract class CodeNode : Node
    {
        public abstract string Modifiers { get; }
    }
    
    public abstract class MemberNode : CodeNode { }
}