using FluentAssertions;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Triples;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Triples.Equality
{
    public class InvokeTripleTests : EqualityTest
    {
        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same(InvokeTriple tripleA,
            InvokeTriple tripleB)
        {
            tripleA.Should().Be(tripleB);
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_Equals(InvokeTriple tripleA,
            InvokeTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_HashCode(InvokeTriple tripleA,
            InvokeTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same(InvokeTriple tripleA,
            InvokeTriple tripleB)
        {
            tripleA.Should().NotBe(tripleB);
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_Equals(InvokeTriple tripleA,
            InvokeTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_HashCode(InvokeTriple tripleA,
            InvokeTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeFalse();
        }

        public static IEnumerable<object[]> IdenticalTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<InvokeTriple, MethodNode, InvocationNode>(i),
                    TripleGenerator<InvokeTriple, MethodNode, InvocationNode>(i)
                };
            }
        }

        public static IEnumerable<object[]> DifferentTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<InvokeTriple, MethodNode, InvocationNode>(i),
                    TripleGenerator<InvokeTriple, MethodNode, InvocationNode>(i + 1)
                };
            }
        }
    }
}