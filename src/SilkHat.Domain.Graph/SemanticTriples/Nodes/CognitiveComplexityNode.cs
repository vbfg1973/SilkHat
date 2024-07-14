using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.SemanticTriples.Nodes
{
    public class CognitiveComplexityNode(int complexityScore) : Node, IEquatable<CognitiveComplexityNode>
    {
        public int ComplexityScore { get; } = complexityScore;
        public override string Label => "CognitiveComplexity";
        public override string FullName { get; } = complexityScore.ToString("D10");
        public override string Name { get; } = complexityScore.ToString("D10");

        public bool Equals(CognitiveComplexityNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ComplexityScore == other.ComplexityScore && FullName == other.FullName && Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CognitiveComplexityNode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ComplexityScore, FullName, Name);
        }
    }
}