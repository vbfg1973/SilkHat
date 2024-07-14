using QuikGraph;
using SilkHat.Domain.Graph.GraphEngine.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.GraphEngine.GraphAnalysers
{
    public class InterfaceImplementations : BaseTripleGraphAnalyzer
    {
        public IEnumerable<ClassNode> FindImplementationsOfInterface(string fullName)
        {
            List<InterfaceNode> matchingInterfaces = AdjacencyGraph!
                .Vertices
                .OfType<InterfaceNode>()
                .Where(x => x.FullName == fullName)
                .ToList();

            foreach (TaggedEdge<Node, string>? taggedEdge in matchingInterfaces.SelectMany(interfaceNode =>
                         AdjacencyGraph.OutEdges(interfaceNode)
                             .Where(x => x.Tag == "OF_TYPE" && x.Target is ClassNode)))
            {
                yield return (taggedEdge.Target as ClassNode)!;
            }
        }
    }
}