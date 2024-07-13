using FluentAssertions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.Tests.TriplesTests.NodeTests
{
    public class ClassNodeEqualityTests
    {
        public static IEnumerable<object[]> ClassNodeIdentical()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new ClassNode(fullName, name, modifiers),
                new ClassNode(fullName, name, modifiers)
            };
        }

        public static IEnumerable<object[]> ClassNodeNameDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string[] modifiers = ["public", "static"];


            yield return new[]
            {
                new ClassNode(fullName, name, modifiers),
                new ClassNode(fullName + "modifier", name, modifiers)
            };

            yield return new[]
            {
                new ClassNode(fullName, name, modifiers),
                new ClassNode(fullName, name + "modifier", modifiers)
            };
        }

        public static IEnumerable<object[]> ClassNodeModifiersDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string[] modifiersOne = ["public", "static"];
            string[] modifiersTwo = ["public"];
            string[] modifiersThree = ["private", "static"];

            yield return new[]
            {
                new ClassNode(fullName, name, modifiersOne),
                new ClassNode(fullName, name, modifiersTwo)
            };

            yield return new[]
            {
                new ClassNode(fullName, name, modifiersTwo),
                new ClassNode(fullName, name, modifiersThree)
            };

            yield return new[]
            {
                new ClassNode(fullName, name, modifiersOne),
                new ClassNode(fullName, name, modifiersThree)
            };
        }

        #region Identical

        [Theory]
        [MemberData(nameof(ClassNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().Be(firstNode);
        }

        [Theory]
        [MemberData(nameof(ClassNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ClassNodeIdentical))]
        public void Given_Identical_Nodes_HashCodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().Be(firstNode.GetHashCode());
        }

        #endregion

        #region Differs

        [Theory]
        [MemberData(nameof(ClassNodeNameDifferent))]
        [MemberData(nameof(ClassNodeModifiersDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().NotBe(firstNode);
        }

        [Theory]
        [MemberData(nameof(ClassNodeNameDifferent))]
        [MemberData(nameof(ClassNodeModifiersDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ClassNodeNameDifferent))]
        [MemberData(nameof(ClassNodeModifiersDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Sign(Node firstNode, Node secondNode)
        {
            (firstNode == secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ClassNodeNameDifferent))]
        [MemberData(nameof(ClassNodeModifiersDifferent))]
        public void Given_Different_Nodes_HashCodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().NotBe(firstNode.GetHashCode());
        }

        #endregion
    }
}