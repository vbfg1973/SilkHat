using FluentAssertions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Nodes.Equality
{
    public class InvocationNodeEqualityTests
    {
        public static IEnumerable<object[]> InvocationNodeIdentical()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            (string name, string type)[] args = [("firstArgument", "string")];
            string returnType = "void";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1),
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1)
            };
        }

        public static IEnumerable<object[]> InvocationNodeNameDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            (string name, string type)[] args = [("firstArgument", "string")];
            string returnType = "void";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1),
                new InvocationNode(CreateMethodNode(fullName + "modified", name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1)
            };

            yield return new[]
            {
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1),
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName + "modified", name, args, returnType, modifiers), 1)
            };

            yield return new[]
            {
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1),
                new InvocationNode(CreateMethodNode(fullName, name + "modified", args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1)
            };

            yield return new[]
            {
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1),
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name + "modified", args, returnType, modifiers), 1)
            };
        }


        public static IEnumerable<object[]> InvocationNodeModifiersDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            (string name, string type)[] args = [("firstArgument", "string")];
            string returnType = "void";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1),

                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, new[] { "public", "void" }),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1)
            };

            yield return new[]
            {
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1),

                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, new[] { "public", "void" }), 1)
            };
        }

        public static IEnumerable<object[]> InvocationNodeReturnTypeDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            (string name, string type)[] args = [("firstArgument", "string")];
            string returnType = "void";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1),

                new InvocationNode(CreateMethodNode(fullName, name, args, "bool", modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1)
            };

            yield return new[]
            {
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1),

                new InvocationNode(CreateMethodNode(fullName, name, args, "int", modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1)
            };
        }

        public static IEnumerable<object[]> InvocationNodeLocationDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            (string name, string type)[] args = [("firstArgument", "string")];
            string returnType = "void";
            string[] modifiers = { "public", "static" };

            yield return new[]
            {
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1),

                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 2)
            };

            yield return new[]
            {
                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 2),

                new InvocationNode(CreateMethodNode(fullName, name, args, returnType, modifiers),
                    CreateMethodNode(fullName, name, args, returnType, modifiers), 1)
            };
        }

        private static MethodNode CreateMethodNode(string fullName, string name, (string name, string type)[] args,
            string returnType, string[] modifiers)
        {
            return new MethodNode(fullName, name, args, returnType, modifiers);
        }

        #region Identical

        [Theory]
        [MemberData(nameof(InvocationNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().Be(firstNode);
        }

        [Theory]
        [MemberData(nameof(InvocationNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(InvocationNodeIdentical))]
        public void Given_Identical_Nodes_HashCodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().Be(firstNode.GetHashCode());
        }

        #endregion

        #region Differs

        [Theory]
        [MemberData(nameof(InvocationNodeNameDifferent))]
        [MemberData(nameof(InvocationNodeModifiersDifferent))]
        [MemberData(nameof(InvocationNodeReturnTypeDifferent))]
        [MemberData(nameof(InvocationNodeLocationDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().NotBe(firstNode);
        }

        [Theory]
        [MemberData(nameof(InvocationNodeNameDifferent))]
        [MemberData(nameof(InvocationNodeModifiersDifferent))]
        [MemberData(nameof(InvocationNodeReturnTypeDifferent))]
        [MemberData(nameof(InvocationNodeLocationDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(InvocationNodeNameDifferent))]
        [MemberData(nameof(InvocationNodeModifiersDifferent))]
        [MemberData(nameof(InvocationNodeReturnTypeDifferent))]
        [MemberData(nameof(InvocationNodeLocationDifferent))]
        public void Given_Different_Nodes_HashCodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().NotBe(firstNode.GetHashCode());
        }

        #endregion
    }
}