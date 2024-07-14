using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.Graph.GraphEngine.Abstract
{
    public interface ITripleGraph
    {
        Task LoadTriples(IEnumerable<Triple> triples);
        bool ContainsNode(Node node);
        internal void SetGraph(BaseTripleGraphAnalyzer baseTripleGraphAnalyzer);
    }
}