using System.Collections.Concurrent;
using QuikGraph;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.GraphEngine
{
    public interface ITripleGraph
    {
        Task LoadTriples(IEnumerable<Triple> triples);
    }

    public class TripleGraph : ITripleGraph
    {
        private readonly AdjacencyGraph<Node, TaggedEdge<Node, string>> _adjacencyGraph = new();
        private readonly ConcurrentDictionary<Node, Node> _nodes = new();
        private readonly ConcurrentDictionary<int, Node> _nodesByHashCode = new();

        public async Task LoadTriples(IEnumerable<Triple> triples)
        {
            foreach (Triple triple in triples)
            {
                TryAddNode(triple.NodeA, out Node? nodeA);
                TryAddNode(triple.NodeA, out Node? nodeB);
                string relationshipType = triple.Relationship.Type;

                TaggedEdge<Node, string> edge = new TaggedEdge<Node, string>(nodeA!, nodeB!, relationshipType);

                _adjacencyGraph.AddVerticesAndEdge(edge);
            }

            await Console.Error.WriteLineAsync(
                $"Edge count: {_adjacencyGraph.EdgeCount} Vertex count: {_adjacencyGraph.VertexCount}");
            await Console.Error.WriteLineAsync($"{_nodes.Keys.Count}");
        }

        private bool TryAddNode(Node node, out Node? normalisedNode)
        {
            normalisedNode = null!;
            if (!_nodes.ContainsKey(node))
            {
                _nodes.TryAdd(node, node);

                if (!_nodesByHashCode.ContainsKey(node.GetHashCode()))
                {
                    _nodesByHashCode.TryAdd(node.GetHashCode(), node);
                }

                else
                {
                    var collideNode = _nodesByHashCode[node.GetHashCode()];
                    Console.Error.WriteLine($"Node adding by Pk: {node.GetHashCode()} - {node.FullName}");
                    Console.Error.WriteLine($"Node collided by Pk: {collideNode.GetHashCode()} - {collideNode.FullName}");
                    Console.Error.WriteLine();
                }
            }
            
            if (!_nodes.TryGetValue(node, out Node? retrieved)) return false;

            normalisedNode = retrieved;

            return true;
        }
    }
}