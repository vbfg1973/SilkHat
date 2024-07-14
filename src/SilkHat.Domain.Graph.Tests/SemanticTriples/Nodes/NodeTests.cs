using FluentAssertions;
using SilkHat.Domain.Graph.Tests.SemanticTriples.Nodes.Data;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Nodes
{
    public class NodeTests
    {
        [Theory]
        [ClassData(typeof(AllInstantiableTypesInheritingFromNodeClassData))]
        public void Given_Type_Inheriting_From_Node_Has_Proper_Naming(Type t)
        {
            t.Name.Should().EndWith("Node");
        }
        
        [Theory]
        [ClassData(typeof(AllInstantiableTypesInheritingFromNodeClassData))]
        public void Given_Type_Inheriting_From_Node_Implements_IEquatable(Type t)
        {
            t.Should().BeAssignableTo(typeof(IEquatable<>));
        }
    }
}
