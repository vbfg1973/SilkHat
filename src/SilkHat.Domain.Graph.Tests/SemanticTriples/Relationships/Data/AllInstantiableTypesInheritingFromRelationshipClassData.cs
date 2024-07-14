using System.Collections;
using System.Reflection;
using SilkHat.Domain.Graph.Support;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Relationships.Data
{
    public class AllInstantiableTypesInheritingFromRelationshipClassData : IEnumerable<object[]>
    {
        private readonly Assembly _assembly = DomainGraphAssemblyReference.Assembly;

        public IEnumerator<object[]> GetEnumerator()
        {
            // Grab types from assembly
            Type[] types = _assembly.GetExportedTypes();

            // Only types that inherit from Node
            foreach (Type type in types.Where(x => x.IsSubclassOf(typeof(Relationship)) && !x.IsAbstract))
            {
                yield return new object[]
                {
                    type
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}