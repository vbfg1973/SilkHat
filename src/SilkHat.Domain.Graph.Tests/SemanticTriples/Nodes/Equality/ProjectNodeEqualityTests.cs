﻿using FluentAssertions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Nodes.Equality
{
    public class ProjectNodeEqualityTests
    {
        public static IEnumerable<object[]> ProjectNodeIdentical()
        {
            string fullName = "This is a full name";
            string name = "This is a name";

            yield return new[]
            {
                new ProjectNode(fullName, name),
                new ProjectNode(fullName, name)
            };
        }

        public static IEnumerable<object[]> ProjectNodeNameDifferent()
        {
            string fullName = "This is a full name";
            string name = "This is a name";

            yield return new[]
            {
                new ProjectNode(fullName, name),
                new ProjectNode(fullName + "modifier", name)
            };

            yield return new[]
            {
                new ProjectNode(fullName, name),
                new ProjectNode(fullName, name + "modifier")
            };
        }

        #region Identical

        [Theory]
        [MemberData(nameof(ProjectNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().Be(firstNode);
        }

        [Theory]
        [MemberData(nameof(ProjectNodeIdentical))]
        public void Given_Identical_Nodes_Are_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ProjectNodeIdentical))]
        public void Given_Identical_Nodes_HashCodes_Are_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().Be(firstNode.GetHashCode());
        }

        #endregion

        #region Differs

        [Theory]
        [MemberData(nameof(ProjectNodeNameDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.Should().NotBe(firstNode);
        }

        [Theory]
        [MemberData(nameof(ProjectNodeNameDifferent))]
        public void Given_Different_Nodes_Are_Not_Equal_Via_Equals_Method(Node firstNode, Node secondNode)
        {
            firstNode.Equals(secondNode).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ProjectNodeNameDifferent))]
        public void Given_Different_Nodes_HashCodes_Are_Not_Equal(Node firstNode, Node secondNode)
        {
            secondNode.GetHashCode().Should().NotBe(firstNode.GetHashCode());
        }

        #endregion
    }
}