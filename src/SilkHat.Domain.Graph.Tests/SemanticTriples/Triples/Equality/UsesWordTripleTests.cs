using FluentAssertions;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Triples;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Triples.Equality
{
    public class UsesWordTripleTests : EqualityTest
    {
        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same(UsesWordTriple tripleA,
            UsesWordTriple tripleB)
        {
            tripleA.Should().Be(tripleB);
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_Equals(UsesWordTriple tripleA,
            UsesWordTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_HashCode(UsesWordTriple tripleA,
            UsesWordTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same(UsesWordTriple tripleA,
            UsesWordTriple tripleB)
        {
            tripleA.Should().NotBe(tripleB);
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_Equals(UsesWordTriple tripleA,
            UsesWordTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_HashCode(UsesWordTriple tripleA,
            UsesWordTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeFalse();
        }

        public static IEnumerable<object[]> IdenticalTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<UsesWordTriple, ClassNode, WordNode>(i),
                    TripleGenerator<UsesWordTriple, ClassNode, WordNode>(i)
                };
                
                yield return new[]
                {
                    TripleGenerator<UsesWordTriple, RecordNode, WordNode>(i),
                    TripleGenerator<UsesWordTriple, RecordNode, WordNode>(i)
                };
                
                yield return new[]
                {
                    TripleGenerator<UsesWordTriple, PropertyNode, WordNode>(i),
                    TripleGenerator<UsesWordTriple, PropertyNode, WordNode>(i)
                };
                
                yield return new[]
                {
                    TripleGenerator<UsesWordTriple, InterfaceNode, WordNode>(i),
                    TripleGenerator<UsesWordTriple, InterfaceNode, WordNode>(i)
                };
            }
        }

        public static IEnumerable<object[]> DifferentTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<UsesWordTriple, ClassNode, WordNode>(i),
                    TripleGenerator<UsesWordTriple, ClassNode, WordNode>(i + 1)
                };
                
                yield return new[]
                {
                    TripleGenerator<UsesWordTriple, RecordNode, WordNode>(i),
                    TripleGenerator<UsesWordTriple, RecordNode, WordNode>(i + 1)
                };
                
                yield return new[]
                {
                    TripleGenerator<UsesWordTriple, PropertyNode, WordNode>(i),
                    TripleGenerator<UsesWordTriple, PropertyNode, WordNode>(i + 1)
                };
                
                yield return new[]
                {
                    TripleGenerator<UsesWordTriple, InterfaceNode, WordNode>(i),
                    TripleGenerator<UsesWordTriple, InterfaceNode, WordNode>(i + 1)
                };
            }
        }
    }
}