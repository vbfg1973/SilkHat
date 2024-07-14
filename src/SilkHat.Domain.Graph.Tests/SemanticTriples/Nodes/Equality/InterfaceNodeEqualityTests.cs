using FluentAssertions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Nodes.Equality
{
    public class InterfaceNodeEqualityTests
    {
        public static IEnumerable<object[]> InterfaceNodeIdentical()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new InterfaceNode(fullName, name, modifiers),
                new InterfaceNode(fullName, name, modifiers)
            };
        }

        public static IEnumerable<object[]> InterfaceNodeNameDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string[] modifiers = ["public", "static"];


            yield return new[]
            {
                new InterfaceNode(fullName, name, modifiers),
                new InterfaceNode(fullName + "modifier", name, modifiers)
            };

            yield return new[]
            {
                new InterfaceNode(fullName, name, modifiers),
                new InterfaceNode(fullName, name + "modifier", modifiers)
            };
        }

        public static IEnumerable<object[]> InterfaceNodeModifiersDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string[] modifiersOne = ["public", "static"];
            string[] modifiersTwo = ["public"];
            string[] modifiersThree = ["private", "static"];

            yield return new[]
            {
                new InterfaceNode(fullName, name, modifiersOne),
                new InterfaceNode(fullName, name, modifiersTwo)
            };

            yield return new[]
            {
                new InterfaceNode(fullName, name, modifiersTwo),
                new InterfaceNode(fullName, name, modifiersThree)
            };

            yield return new[]
            {
                new InterfaceNode(fullName, name, modifiersOne),
                new InterfaceNode(fullName, name, modifiersThree)
            };
        }

        #region Identical

        [Theory]
        [MemberData(nameof(InterfaceNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().Be(firstNode);
        }

        [Theory]
        [MemberData(nameof(InterfaceNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(InterfaceNodeIdentical))]
        public void Given_Identical_Nodes_HashCodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().Be(firstNode.GetHashCode());
        }

        #endregion

        #region Differs

        [Theory]
        [MemberData(nameof(InterfaceNodeNameDifferent))]
        [MemberData(nameof(InterfaceNodeModifiersDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().NotBe(firstNode);
        }

        [Theory]
        [MemberData(nameof(InterfaceNodeNameDifferent))]
        [MemberData(nameof(InterfaceNodeModifiersDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(InterfaceNodeNameDifferent))]
        [MemberData(nameof(InterfaceNodeModifiersDifferent))]
        public void Given_Different_Nodes_HashCodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().NotBe(firstNode.GetHashCode());
        }

        #endregion
    }
}