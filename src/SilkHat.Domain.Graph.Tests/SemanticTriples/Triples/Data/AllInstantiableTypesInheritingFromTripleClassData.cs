using System.Collections;
using System.Reflection;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;
using SilkHat.Domain.Graph.Support;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples.Triples.Data
{
    public class AllInstantiableTypesInheritingFromTripleClassData : IEnumerable<object[]>
    {
        private readonly Assembly _assembly = DomainGraphAssemblyReference.Assembly;

        public IEnumerator<object[]> GetEnumerator()
        {
            // Grab types from assembly
            Type[] types = _assembly.GetExportedTypes();

            // Only types that inherit from Node
            foreach (Type type in types.Where(x => x.IsSubclassOf(typeof(Triple)) && !x.IsAbstract))
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