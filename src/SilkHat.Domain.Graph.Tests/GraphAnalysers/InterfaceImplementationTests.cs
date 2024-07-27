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

            TripleGraphService graphService = new();
            await graphService.LoadTriples(new[] { triple });

            graphService.ContainsNode(triple.NodeA);
            graphService.ContainsNode(triple.NodeB);

            TripleGraphAnalyserFactory tripleFactory = new TripleGraphAnalyserFactory(graphService);
            InterfaceImplementationsGraphAnalyzer analyser = tripleFactory.CreateTripleGraphAnalyzer<InterfaceImplementationsGraphAnalyzer>();
            List<ClassNode> implementations = analyser
                .FindImplementationsOfInterface("TestNamespace.ITestClass")
                .ToList();

            implementations.Count.Should().Be(1);
            implementations.First().Should().Be(triple.NodeB);
        }
    }
}