using FluentAssertions;
using SilkHat.Domain.Graph.Tests.SemanticTriples.Relationships.Data;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Relationships
{
    public class RelationshipTests
    {
        [Theory]
        [ClassData(typeof(AllInstantiableTypesInheritingFromRelationshipClassData))]
        public void Given_Type_Inheriting_From_Relationship_Has_Proper_Naming(Type t)
        {
            t.Name.Should().EndWith("Relationship");
        }

        [Theory]
        [ClassData(typeof(AllInstantiableTypesInheritingFromRelationshipClassData))]
        public void Given_Type_Inheriting_From_Relationship_Implements_IEquatable(Type t)
        {
            t.Should().BeAssignableTo(typeof(IEquatable<>));
        }
    }
}