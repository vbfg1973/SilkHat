using FluentAssertions;
using SilkHat.Domain.Graph.GraphEngine;
using SilkHat.Domain.Graph.GraphEngine.GraphAnalysers;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Triples;

namespace SilkHat.Domain.Graph.Tests.GraphAnalysers
{
    public class InterfaceImplementationTests
    {
        [Fact]
        public async Task SimpleFindImplementations()
        {
            OfTypeTriple triple = new(
                new InterfaceNode("TestNamespace.ITestClass", "ITestClass", new[] { "public" }),
                new ClassNode("TestNamespace.TestClass", "TestClass", new[] { "public" })
            );

            TripleGraph graph = new();
            await graph.LoadTriples(new[] { triple });

            graph.ContainsNode(triple.NodeA);
            graph.ContainsNode(triple.NodeB);

            TripleGraphAnalyserFactory tripleFactory = new TripleGraphAnalyserFactory(graph);
            InterfaceImplementationsGraphAnalyzer analyser = tripleFactory.CreateTripleGraphAnalyzer<InterfaceImplementationsGraphAnalyzer>();
            List<ClassNode> implementations = analyser
                .FindImplementationsOfInterface("TestNamespace.ITestClass")
                .ToList();

            implementations.Count.Should().Be(1);
            implementations.First().Should().Be(triple.NodeB);
        }
    }
}