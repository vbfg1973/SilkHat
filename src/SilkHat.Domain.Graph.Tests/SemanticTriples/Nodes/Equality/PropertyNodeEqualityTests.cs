using FluentAssertions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Nodes.Equality
{
    public class PropertyNodeEqualityTests
    {
        public static IEnumerable<object[]> PropertyNodeIdentical()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string returnType = "void";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new PropertyNode(fullName, name, returnType, modifiers),
                new PropertyNode(fullName, name, returnType, modifiers)
            };
        }

        public static IEnumerable<object[]> PropertyNodeNameDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string returnType = "void";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new PropertyNode(fullName, name,  returnType, modifiers),
                new PropertyNode(fullName, name + "modifiers",  returnType, modifiers)
            };


            yield return new[]
            {
                new PropertyNode(fullName, name,  returnType, modifiers),
                new PropertyNode(fullName + "modifiers", name,  returnType, modifiers)
            };
        }

        public static IEnumerable<object[]> PropertyNodeReturnTypeDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new PropertyNode(fullName, name,  "int", modifiers),
                new PropertyNode(fullName, name + "modifiers",  "string", modifiers)
            };


            yield return new[]
            {
                new PropertyNode(fullName, name,  "bool", modifiers),
                new PropertyNode(fullName + "modifiers", name,  "float", modifiers)
            };
        }

        public static IEnumerable<object[]> PropertyNodeModifiersDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string returnType = "void";

            string[] modifiersOne = ["public", "static"];
            string[] modifiersTwo = ["public"];
            string[] modifiersThree = ["private", "static"];

            yield return new[]
            {
                new PropertyNode(fullName, name,  returnType, modifiersOne),
                new PropertyNode(fullName, name,  returnType, modifiersTwo)
            };

            yield return new[]
            {
                new PropertyNode(fullName, name,  returnType, modifiersTwo),
                new PropertyNode(fullName, name,  returnType, modifiersThree)
            };

            yield return new[]
            {
                new PropertyNode(fullName, name,  returnType, modifiersOne),
                new PropertyNode(fullName, name,  returnType, modifiersThree)
            };
        }

        #region Identical

        [Theory]
        [MemberData(nameof(PropertyNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().Be(firstNode);
        }

        [Theory]
        [MemberData(nameof(PropertyNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal_Via_Equals_Property(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(PropertyNodeIdentical))]
        public void Given_Identical_Nodes_HashCodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().Be(firstNode.GetHashCode());
        }

        #endregion

        #region Differs

        [Theory]
        [MemberData(nameof(PropertyNodeNameDifferent))]
        [MemberData(nameof(PropertyNodeModifiersDifferent))]
        [MemberData(nameof(PropertyNodeReturnTypeDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().NotBe(firstNode);
        }

        [Theory]
        [MemberData(nameof(PropertyNodeNameDifferent))]
        [MemberData(nameof(PropertyNodeModifiersDifferent))]
        [MemberData(nameof(PropertyNodeReturnTypeDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Property(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(PropertyNodeNameDifferent))]
        [MemberData(nameof(PropertyNodeModifiersDifferent))]
        [MemberData(nameof(PropertyNodeReturnTypeDifferent))]
        public void Given_Different_Nodes_HashCodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().NotBe(firstNode.GetHashCode());
        }

        #endregion
    }
}