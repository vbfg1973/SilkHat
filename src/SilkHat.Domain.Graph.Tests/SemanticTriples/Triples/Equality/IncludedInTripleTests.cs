using FluentAssertions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Triples;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Triples.Equality
{
    public class IncludedInTripleTests : EqualityTest
    {
        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same(IncludedInTriple tripleA,
            IncludedInTriple tripleB)
        {
            tripleA.Should().Be(tripleB);
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_Equals(IncludedInTriple tripleA,
            IncludedInTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(IdenticalTriples))]
        public void Given_Two_Identical_Triples_Are_The_Same_Via_HashCode(IncludedInTriple tripleA,
            IncludedInTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same(IncludedInTriple tripleA,
            IncludedInTriple tripleB)
        {
            tripleA.Should().NotBe(tripleB);
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_Equals(IncludedInTriple tripleA,
            IncludedInTriple tripleB)
        {
            tripleA.Equals(tripleB).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(DifferentTriples))]
        public void Given_Two_Identical_Triples_Are_Not_The_Same_Via_HashCode(IncludedInTriple tripleA,
            IncludedInTriple tripleB)
        {
            tripleA.GetHashCode().Equals(tripleB.GetHashCode()).Should().BeFalse();
        }

        public static IEnumerable<object[]> IdenticalTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<IncludedInTriple, ProjectNode, FolderNode>(i),
                    TripleGenerator<IncludedInTriple, ProjectNode, FolderNode>(i)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<IncludedInTriple, FolderNode, FolderNode>(i),
                    TripleGenerator<IncludedInTriple, FolderNode, FolderNode>(i)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<IncludedInTriple, FileNode, FolderNode>(i),
                    TripleGenerator<IncludedInTriple, FileNode, FolderNode>(i)
                };
            }
        }

        public static IEnumerable<object[]> DifferentTriples()
        {
            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<IncludedInTriple, ProjectNode, FolderNode>(i),
                    TripleGenerator<IncludedInTriple, ProjectNode, FolderNode>(i + 1)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<IncludedInTriple, FolderNode, FolderNode>(i),
                    TripleGenerator<IncludedInTriple, FolderNode, FolderNode>(i + 1)
                };
            }

            for (int i = 1; i < 10; i++)
            {
                yield return new[]
                {
                    TripleGenerator<IncludedInTriple, FileNode, FolderNode>(i),
                    TripleGenerator<IncludedInTriple, FileNode, FolderNode>(i + 1)
                };
            }
        }
    }
}