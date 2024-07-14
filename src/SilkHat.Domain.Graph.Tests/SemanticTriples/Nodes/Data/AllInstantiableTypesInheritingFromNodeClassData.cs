using System.Collections;
using System.Reflection;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.Support;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Nodes.Data
{
    public class AllInstantiableTypesInheritingFromNodeClassData : IEnumerable<object[]>
    {
        private readonly Assembly _assembly = DomainGraphAssemblyReference.Assembly;

        public IEnumerator<object[]> GetEnumerator()
        {
            // Grab types from assembly
            Type[] types = _assembly.GetExportedTypes();

            // Only types that inherit from Node
            foreach (Type type in types.Where(x => x.IsSubclassOf(typeof(Node)) && !x.IsAbstract))
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