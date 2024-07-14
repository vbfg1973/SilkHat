using System.Collections.Concurrent;
using QuikGraph;
using SilkHat.Domain.Graph.GraphEngine.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.Graph.GraphEngine
{
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
                TryAddNode(triple.NodeB, out Node? nodeB);
                string relationshipType = triple.Relationship.Type;

                TaggedEdge<Node, string> edge = new(nodeA!, nodeB!, relationshipType);

                _adjacencyGraph.AddVertex(nodeA!);
                _adjacencyGraph.AddVertex(nodeB!);
                _adjacencyGraph.AddEdge(edge);
            }

            await Console.Error.WriteLineAsync(
                $"Edge count: {_adjacencyGraph.EdgeCount} Vertex count: {_adjacencyGraph.VertexCount}");
            await Console.Error.WriteLineAsync($"{_nodes.Keys.Count}");
        }

        public bool ContainsNode(Node node)
        {
            return _nodes.ContainsKey(node) && _adjacencyGraph.ContainsVertex(node);
        }

        public void SetGraph(BaseTripleGraphAnalyzer baseTripleGraphAnalyzer)
        {
            baseTripleGraphAnalyzer.SetGraph(_adjacencyGraph);
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
                    Node collideNode = _nodesByHashCode[node.GetHashCode()];
                    HashSet<Node> hashSet = [node, collideNode];
                    
                    Console.Error.WriteLine($"Node adding by HashCode: {node.GetHashCode()} - {node.FullName} - {node.GetType()}");
                    Console.Error.WriteLine($"Node collided by HashCode: {collideNode.GetHashCode()} - {collideNode.FullName} - {node.GetType()}");

                    Console.Error.WriteLine($"Do they equal?: {node.Equals(collideNode)}");
                    Console.Error.WriteLine($"HashSet size: {hashSet.Count}");
                    Console.Error.WriteLine();
                }
            }

            if (!_nodes.TryGetValue(node, out Node? retrieved)) return false;

            normalisedNode = retrieved;

            return true;
        }
    }
}