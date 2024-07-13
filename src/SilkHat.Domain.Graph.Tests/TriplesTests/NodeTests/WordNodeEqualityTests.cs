using FluentAssertions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.Tests.TriplesTests.NodeTests
{
    public class WordNodeEqualityTests
    {
        public static IEnumerable<object[]> WordNodeIdentical()
        {
            string fullName = "This is a full name";
            string name = "This is a name";

            yield return new[]
            {
                new WordNode(fullName, name),
                new WordNode(fullName, name)
            };
        }

        public static IEnumerable<object[]> WordNodeNameDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";

            yield return new[]
            {
                new WordNode(fullName, name),
                new WordNode(fullName + "modifier", name)
            };

            yield return new[]
            {
                new WordNode(fullName, name),
                new WordNode(fullName, name + "modifier")
            };
        }

        #region Identical

        [Theory]
        [MemberData(nameof(WordNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().Be(firstNode);
        }

        [Theory]
        [MemberData(nameof(WordNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(WordNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal_Via_Equals_Sign(Node firstNode, Node secondNode)
        {
            (firstNode == secondNode).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(WordNodeIdentical))]
        public void Given_Identical_Nodes_HashCodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().Be(firstNode.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(WordNodeIdentical))]
        public void Given_Identical_Nodes_PrimaryKeys_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Pk.Should().Be(firstNode.Pk);
        }

        #endregion

        #region Differs

        [Theory]
        [MemberData(nameof(WordNodeNameDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().NotBe(firstNode);
        }

        [Theory]
        [MemberData(nameof(WordNodeNameDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(WordNodeNameDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Sign(Node firstNode, Node secondNode)
        {
            (firstNode == secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(WordNodeNameDifferent))]
        public void Given_Different_Nodes_HashCodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().NotBe(firstNode.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(WordNodeNameDifferent))]
        public void Given_Different_Nodes_PrimaryKeys_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Pk.Should().NotBe(firstNode.Pk);
        }

        #endregion
    }
}