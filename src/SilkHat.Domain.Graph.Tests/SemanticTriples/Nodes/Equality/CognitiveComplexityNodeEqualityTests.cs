using FluentAssertions;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Nodes.Equality
{
    public class CognitiveComplexityNodeEqualityTests
    {
        public static IEnumerable<object[]> CognitiveComplexityNodeIdentical()
        {
            int complexity = 10;

            yield return new[]
            {
                new CognitiveComplexityNode(complexity),
                new CognitiveComplexityNode(complexity)
            };
        }

        public static IEnumerable<object[]> CognitiveComplexityNodeComplexityDifferent()
        {
            int complexity = 10;

            yield return new[]
            {
                new CognitiveComplexityNode(complexity),
                new CognitiveComplexityNode(complexity + 1)
            };

            yield return new[]
            {
                new CognitiveComplexityNode(complexity),
                new CognitiveComplexityNode(complexity - 1)
            };
        }

        #region Identical

        [Theory]
        [MemberData(nameof(CognitiveComplexityNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().Be(firstNode);
        }

        [Theory]
        [MemberData(nameof(CognitiveComplexityNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(CognitiveComplexityNodeIdentical))]
        public void Given_Identical_Nodes_HashCodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().Be(firstNode.GetHashCode());
        }

        #endregion

        #region Differs

        [Theory]
        [MemberData(nameof(CognitiveComplexityNodeComplexityDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().NotBe(firstNode);
        }

        [Theory]
        [MemberData(nameof(CognitiveComplexityNodeComplexityDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(CognitiveComplexityNodeComplexityDifferent))]
        public void Given_Different_Nodes_HashCodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().NotBe(firstNode.GetHashCode());
        }

        #endregion
    }
}