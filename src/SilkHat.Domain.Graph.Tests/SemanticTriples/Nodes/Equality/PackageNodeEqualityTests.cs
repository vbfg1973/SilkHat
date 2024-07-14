using FluentAssertions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Nodes.Equality
{
    public class PackageNodeEqualityTests
    {
        public static IEnumerable<object[]> PackageNodeIdentical()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string version = "1.0";
            yield return new[]
            {
                new PackageNode(fullName, name, version),
                new PackageNode(fullName, name, version)
            };
        }

        public static IEnumerable<object[]> PackageNodeNameDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string version = "1.0";
            yield return new[]
            {
                new PackageNode(fullName, name, version),
                new PackageNode(fullName + "modifier", name, version)
            };

            yield return new[]
            {
                new PackageNode(fullName, name, version),
                new PackageNode(fullName, name + "modifier", version)
            };
        }

        public static IEnumerable<object[]> PackageNodeVersionDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";
            string version = "1.0";

            yield return new[]
            {
                new PackageNode(fullName, name, version),
                new PackageNode(fullName, name, version + ".1")
            };
        }

        #region Identical

        [Theory]
        [MemberData(nameof(PackageNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().Be(firstNode);
        }

        [Theory]
        [MemberData(nameof(PackageNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(PackageNodeIdentical))]
        public void Given_Identical_Nodes_HashCodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().Be(firstNode.GetHashCode());
        }

        #endregion

        #region Differs

        [Theory]
        [MemberData(nameof(PackageNodeNameDifferent))]
        [MemberData(nameof(PackageNodeVersionDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().NotBe(firstNode);
        }

        [Theory]
        [MemberData(nameof(PackageNodeNameDifferent))]
        [MemberData(nameof(PackageNodeVersionDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(PackageNodeNameDifferent))]
        [MemberData(nameof(PackageNodeVersionDifferent))]
        public void Given_Different_Nodes_HashCodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().NotBe(firstNode.GetHashCode());
        }

        #endregion
    }
}