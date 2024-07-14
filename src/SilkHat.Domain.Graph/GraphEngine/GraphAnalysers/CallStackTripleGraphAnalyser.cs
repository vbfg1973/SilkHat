using QuikGraph;
using SilkHat.Domain.Graph.GraphEngine.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.GraphEngine.GraphAnalysers
{
    public class CallStackTripleGraphAnalyser(AdjacencyGraph<Node, TaggedEdge<Node, string>> graph)
        : BaseTripleGraphAnalyzer
    {
    }
}