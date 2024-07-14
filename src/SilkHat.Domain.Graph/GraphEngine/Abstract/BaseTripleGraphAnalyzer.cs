using QuikGraph;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.GraphEngine.Abstract
{
    public abstract class BaseTripleGraphAnalyzer
    {
        protected AdjacencyGraph<Node, TaggedEdge<Node, string>>? AdjacencyGraph;

        public void SetGraph(AdjacencyGraph<Node, TaggedEdge<Node, string>> graph)
        {
            AdjacencyGraph ??= graph;
        }
    }
}