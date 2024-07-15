using QuikGraph;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;

namespace SilkHat.Domain.Graph.GraphEngine.Abstract
{
    public abstract class BaseTripleGraphAnalyzer
    {
        protected AdjacencyGraph<Node, TaggedEdge<Node, Relationship>>? AdjacencyGraph;

        public void SetGraph(AdjacencyGraph<Node, TaggedEdge<Node, Relationship>> graph)
        {
            AdjacencyGraph ??= graph;
        }
    }
}