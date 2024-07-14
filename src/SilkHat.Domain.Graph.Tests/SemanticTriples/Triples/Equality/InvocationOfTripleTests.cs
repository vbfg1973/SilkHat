using FluentAssertions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Triples;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Triples.Equality
{
    public class InvocationOfTripleTests : EqualityTest
    {
        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same(InvocationOfTriple tripleA,
            InvocationOfTriple tripleB)
        {
            tripleA.Should().Be(tripleB);
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_Equals(InvocationOfTriple tripleA,
            InvocationOfTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_HashCode(InvocationOfTriple tripleA,
            InvocationOfTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same(InvocationOfTriple tripleA,
            InvocationOfTriple tripleB)
        {
            tripleA.Should().NotBe(tripleB);
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_Equals(InvocationOfTriple tripleA,
            InvocationOfTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_HashCode(InvocationOfTriple tripleA,
            InvocationOfTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeFalse();
        }

        public static IEnumerable<object[]> IdenticalTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<InvocationOfTriple, InvocationNode, MethodNode>(i),
                    TripleGenerator<InvocationOfTriple, InvocationNode, MethodNode>(i)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<InvocationOfTriple, InvocationNode, PropertyNode>(i),
                    TripleGenerator<InvocationOfTriple, InvocationNode, PropertyNode>(i)
                };
            }
        }

        public static IEnumerable<object[]> DifferentTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<InvocationOfTriple, InvocationNode, MethodNode>(i),
                    TripleGenerator<InvocationOfTriple, InvocationNode, MethodNode>(i + 1)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<InvocationOfTriple, InvocationNode, PropertyNode>(i),
                    TripleGenerator<InvocationOfTriple, InvocationNode, PropertyNode>(i + 1)
                };
            }
        }
    }
}