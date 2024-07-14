using AutoBogus;
using Bogus;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.Graph.Tests.SemanticTriples
{
    public abstract class EqualityTest
    {
        protected static T NodeGenerator<T>(int seed) where T : Node
        {
            Faker<T>? faker = new AutoFaker<T>()
                .UseSeed(seed);

            return faker.Generate(1).First();
        }

        protected static T TripleGenerator<T, TU, TV>(int seed)
            where T : Triple
            where TU : Node
            where TV : Node
        {
            TU uNode = NodeGenerator<TU>(seed);
            TV vNode = NodeGenerator<TV>(seed + 1);

            return (T)Activator.CreateInstance(typeof(T), uNode, vNode)!;
        }
    }
}