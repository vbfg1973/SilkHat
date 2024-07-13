using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
{
    public class CognitiveComplexityNode : Node, IEquatable<CognitiveComplexityNode>
    {
        public CognitiveComplexityNode(int complexityScore) : base(complexityScore.ToString("D10"),
            complexityScore.ToString("D10"))
        {
            ComplexityScore = complexityScore;
        }

        public int ComplexityScore { get; }

        public override string Label { get; } = "CognitiveComplexity";

       
        public bool Equals(CognitiveComplexityNode? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && ComplexityScore == other.ComplexityScore && Label == other.Label;
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
            return HashCode.Combine(base.GetHashCode(), ComplexityScore, Label);
        }

        public static bool operator ==(CognitiveComplexityNode? left, CognitiveComplexityNode? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CognitiveComplexityNode? left, CognitiveComplexityNode? right)
        {
            return !Equals(left, right);
        }
    }
}