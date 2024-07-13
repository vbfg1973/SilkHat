using FluentAssertions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.Tests.TriplesTests.NodeTests
{
    public class MethodNodeEqualityTests
    {
        public static IEnumerable<object[]> MethodNodeIdentical()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            (string name, string type)[] args = [("firstArgument", "string")];
            string returnType = "void";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new MethodNode(fullName, name, args, returnType, modifiers),
                new MethodNode(fullName, name, args, returnType, modifiers)
            };
        }

        public static IEnumerable<object[]> MethodNodeNameDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            (string name, string type)[] args = [("firstArgument", "string")];
            string returnType = "void";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new MethodNode(fullName, name, args, returnType, modifiers),
                new MethodNode(fullName, name + "modifiers", args, returnType, modifiers)
            };


            yield return new[]
            {
                new MethodNode(fullName, name, args, returnType, modifiers),
                new MethodNode(fullName + "modifiers", name, args, returnType, modifiers)
            };
        }
        
        public static IEnumerable<object[]> MethodNodeReturnTypeDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            (string name, string type)[] args = [("firstArgument", "string")];
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new MethodNode(fullName, name, args, "int", modifiers),
                new MethodNode(fullName, name + "modifiers", args, "string", modifiers)
            };


            yield return new[]
            {
                new MethodNode(fullName, name, args, "bool", modifiers),
                new MethodNode(fullName + "modifiers", name, args, "float", modifiers)
            };
        }

        public static IEnumerable<object[]> MethodNodeModifiersDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            (string name, string type)[] args = [("firstArgument", "string")];
            string returnType = "void";
            
            string[] modifiersOne = ["public", "static"];
            string[] modifiersTwo = ["public"];
            string[] modifiersThree = ["private", "static"];

            yield return new[]
            {
                new MethodNode(fullName, name, args, returnType, modifiersOne),
                new MethodNode(fullName, name, args, returnType, modifiersTwo),
            };

            yield return new[]
            {
                new MethodNode(fullName, name, args, returnType, modifiersTwo),
                new MethodNode(fullName, name, args, returnType, modifiersThree),
            };

            yield return new[]
            {
                new MethodNode(fullName, name, args, returnType, modifiersOne),
                new MethodNode(fullName, name, args, returnType, modifiersThree),
            };
        }
        
        public static IEnumerable<object[]> MethodNodeArgsDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            (string name, string type)[] argsOne = [("firstArgument", "string")];
            (string name, string type)[] argsTwo = [("firstArgument", "int")];
            string returnType = "void";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new MethodNode(fullName, name, argsOne, returnType, modifiers),
                new MethodNode(fullName, name, argsTwo, returnType, modifiers)
            };
        }

        #region Identical

        [Theory]
        [MemberData(nameof(MethodNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().Be(firstNode);
        }

        [Theory]
        [MemberData(nameof(MethodNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(MethodNodeIdentical))]
        public void Given_Identical_Nodes_HashCodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().Be(firstNode.GetHashCode());
        }

        #endregion

        #region Differs

        [Theory]
        [MemberData(nameof(MethodNodeNameDifferent))]
        [MemberData(nameof(MethodNodeModifiersDifferent))]
        [MemberData(nameof(MethodNodeArgsDifferent))]
        [MemberData(nameof(MethodNodeReturnTypeDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().NotBe(firstNode);
        }

        [Theory]
        [MemberData(nameof(MethodNodeNameDifferent))]
        [MemberData(nameof(MethodNodeModifiersDifferent))]
        [MemberData(nameof(MethodNodeArgsDifferent))]
        [MemberData(nameof(MethodNodeReturnTypeDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(MethodNodeNameDifferent))]
        [MemberData(nameof(MethodNodeModifiersDifferent))]
        [MemberData(nameof(MethodNodeArgsDifferent))]
        [MemberData(nameof(MethodNodeReturnTypeDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Sign(Node firstNode, Node secondNode)
        {
            (firstNode == secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(MethodNodeNameDifferent))]
        [MemberData(nameof(MethodNodeModifiersDifferent))]
        [MemberData(nameof(MethodNodeArgsDifferent))]
        [MemberData(nameof(MethodNodeReturnTypeDifferent))]
        public void Given_Different_Nodes_HashCodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().NotBe(firstNode.GetHashCode());
        }

        #endregion
    }
}