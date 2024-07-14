using FluentAssertions;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Triples;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Triples.Equality
{
    public class BelongsToTripleTests : EqualityTest
    {
        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same(BelongsToTriple tripleA,
            BelongsToTriple tripleB)
        {
            tripleA.Should().Be(tripleB);
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_Equals(BelongsToTriple tripleA,
            BelongsToTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_HashCode(BelongsToTriple tripleA,
            BelongsToTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same(BelongsToTriple tripleA,
            BelongsToTriple tripleB)
        {
            tripleA.Should().NotBe(tripleB);
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_Equals(BelongsToTriple tripleA,
            BelongsToTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_HashCode(BelongsToTriple tripleA,
            BelongsToTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeFalse();
        }

        public static IEnumerable<object[]> IdenticalTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<BelongsToTriple, ClassNode, ProjectNode>(i),
                    TripleGenerator<BelongsToTriple, ClassNode, ProjectNode>(i)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<BelongsToTriple, InterfaceNode, ProjectNode>(i),
                    TripleGenerator<BelongsToTriple, InterfaceNode, ProjectNode>(i)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<BelongsToTriple, RecordNode, ProjectNode>(i),
                    TripleGenerator<BelongsToTriple, RecordNode, ProjectNode>(i)
                };
            }
        }

        public static IEnumerable<object[]> DifferentTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<BelongsToTriple, ClassNode, ProjectNode>(i),
                    TripleGenerator<BelongsToTriple, ClassNode, ProjectNode>(i + 1)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<BelongsToTriple, InterfaceNode, ProjectNode>(i),
                    TripleGenerator<BelongsToTriple, InterfaceNode, ProjectNode>(i + 1)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<BelongsToTriple, RecordNode, ProjectNode>(i),
                    TripleGenerator<BelongsToTriple, RecordNode, ProjectNode>(i + 1)
                };
            }
        }
    }
}