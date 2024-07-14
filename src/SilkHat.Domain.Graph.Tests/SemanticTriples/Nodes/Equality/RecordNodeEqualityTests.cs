using FluentAssertions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Nodes.Equality
{
    public class RecordNodeEqualityTests
    {
        public static IEnumerable<object[]> RecordNodeIdentical()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new RecordNode(fullName, name, modifiers),
                new RecordNode(fullName, name, modifiers)
            };
        }

        public static IEnumerable<object[]> RecordNodeNameDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string[] modifiers = ["public", "static"];


            yield return new[]
            {
                new RecordNode(fullName, name, modifiers),
                new RecordNode(fullName + "modifier", name, modifiers)
            };

            yield return new[]
            {
                new RecordNode(fullName, name, modifiers),
                new RecordNode(fullName, name + "modifier", modifiers)
            };
        }

        public static IEnumerable<object[]> RecordNodeModifiersDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string[] modifiersOne = ["public", "static"];
            string[] modifiersTwo = ["public"];
            string[] modifiersThree = ["private", "static"];

            yield return new[]
            {
                new RecordNode(fullName, name, modifiersOne),
                new RecordNode(fullName, name, modifiersTwo)
            };

            yield return new[]
            {
                new RecordNode(fullName, name, modifiersTwo),
                new RecordNode(fullName, name, modifiersThree)
            };

            yield return new[]
            {
                new RecordNode(fullName, name, modifiersOne),
                new RecordNode(fullName, name, modifiersThree)
            };
        }

        #region Identical

        [Theory]
        [MemberData(nameof(RecordNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().Be(firstNode);
        }

        [Theory]
        [MemberData(nameof(RecordNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(RecordNodeIdentical))]
        public void Given_Identical_Nodes_HashCodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().Be(firstNode.GetHashCode());
        }

        #endregion

        #region Differs

        [Theory]
        [MemberData(nameof(RecordNodeNameDifferent))]
        [MemberData(nameof(RecordNodeModifiersDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().NotBe(firstNode);
        }

        [Theory]
        [MemberData(nameof(RecordNodeNameDifferent))]
        [MemberData(nameof(RecordNodeModifiersDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(RecordNodeNameDifferent))]
        [MemberData(nameof(RecordNodeModifiersDifferent))]
        public void Given_Different_Nodes_HashCodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().NotBe(firstNode.GetHashCode());
        }

        #endregion
    }
}