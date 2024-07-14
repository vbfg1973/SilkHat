using FluentAssertions;
using SilkHat.Domain.Graph.Tests.SemanticTriples.Triples.Data;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Triples
{
    public class TripleTests
    {
        [Theory]
        [ClassData(typeof(AllInstantiableTypesInheritingFromTripleClassData))]
        public void Given_Type_Inheriting_From_Triple_Has_Proper_Naming(Type t)
        {
            t.Name.Should().EndWith("Triple");
        }

        [Theory]
        [ClassData(typeof(AllInstantiableTypesInheritingFromTripleClassData))]
        public void Given_Type_Inheriting_From_Triple_Implements_IEquatable(Type t)
        {
            t.Should().BeAssignableTo(typeof(IEquatable<>));
        }
    }
}