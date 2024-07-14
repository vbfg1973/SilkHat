using FluentAssertions;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Triples;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Triples.Equality
{
    public class OfTypeTripleTests : EqualityTest
    {
        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same(OfTypeTriple tripleA,
            OfTypeTriple tripleB)
        {
            tripleA.Should().Be(tripleB);
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_Equals(OfTypeTriple tripleA,
            OfTypeTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_HashCode(OfTypeTriple tripleA,
            OfTypeTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same(OfTypeTriple tripleA,
            OfTypeTriple tripleB)
        {
            tripleA.Should().NotBe(tripleB);
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_Equals(OfTypeTriple tripleA,
            OfTypeTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_HashCode(OfTypeTriple tripleA,
            OfTypeTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeFalse();
        }

        public static IEnumerable<object[]> IdenticalTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<OfTypeTriple, ClassNode, ClassNode>(i),
                    TripleGenerator<OfTypeTriple, ClassNode, ClassNode>(i)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<OfTypeTriple, ClassNode, InterfaceNode>(i),
                    TripleGenerator<OfTypeTriple, ClassNode, InterfaceNode>(i)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<OfTypeTriple, RecordNode, ClassNode>(i),
                    TripleGenerator<OfTypeTriple, RecordNode, ClassNode>(i)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<OfTypeTriple, RecordNode, InterfaceNode>(i),
                    TripleGenerator<OfTypeTriple, RecordNode, InterfaceNode>(i)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<OfTypeTriple, InterfaceNode, InterfaceNode>(i),
                    TripleGenerator<OfTypeTriple, InterfaceNode, InterfaceNode>(i)
                };
            }
        }

        public static IEnumerable<object[]> DifferentTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<OfTypeTriple, ClassNode, ClassNode>(i),
                    TripleGenerator<OfTypeTriple, ClassNode, ClassNode>(i + 1)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<OfTypeTriple, ClassNode, InterfaceNode>(i),
                    TripleGenerator<OfTypeTriple, ClassNode, InterfaceNode>(i + 1)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<OfTypeTriple, RecordNode, ClassNode>(i),
                    TripleGenerator<OfTypeTriple, RecordNode, ClassNode>(i + 1)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<OfTypeTriple, RecordNode, InterfaceNode>(i),
                    TripleGenerator<OfTypeTriple, RecordNode, InterfaceNode>(i + 1)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<OfTypeTriple, InterfaceNode, InterfaceNode>(i),
                    TripleGenerator<OfTypeTriple, InterfaceNode, InterfaceNode>(i + 1)
                };
            }
        }
    }
}