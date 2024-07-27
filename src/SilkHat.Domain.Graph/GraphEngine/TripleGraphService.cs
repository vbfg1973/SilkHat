using System.Collections.Concurrent;
using CsvHelper.Configuration;
using QuikGraph;
using SilkHat.Domain.Common;
using SilkHat.Domain.Graph.GraphEngine.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Relationships;
using SilkHat.Domain.Graph.SemanticTriples.Relationships.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.Graph.GraphEngine
{
    public sealed class GraphClassMap : ClassMap<TaggedEdge<Node, Relationship>>
    {
        public GraphClassMap()
        {
            Map(m => m.Source.FullName).Name("SourceName").Index(0);
            Map(m => m.Source.Label).Name("SourceLabel").Index(1);
            Map(m => m.Tag!.Type).Name("Relationship").Index(2);
            Map(m => m.Target.FullName).Name("TargetName").Index(3);
            Map(m => m.Target.Label).Name("TargetLabel").Index(4);
        }
    }


    public interface ITripleGraph
    {
        IEnumerable<MethodNode> GetTypeMethods(TypeNode typeNode);
        IEnumerable<PropertyNode> GetTypeProperties(TypeNode typeNode);
        TypeNode GetOwningTypeOfMember(MemberNode memberNode);
        ProjectNode GetOwningProjectOfType(TypeNode typeNode);
        IEnumerable<TypeNode> GetTypesFromProject(ProjectNode projectNode);
        IEnumerable<MethodNode> ConstructedBy(TypeNode typeNode);
        IEnumerable<TypeNode> Constructs(MethodNode methodNode);
        IEnumerable<TypeNode> Declares(FileNode fileNode);
        FileNode DeclaredAt(TypeNode typeNode);
    }

    public sealed class TripleGraph : AdjacencyGraph<Node, TaggedEdge<Node, Relationship>>, ITripleGraph
    {
        public IEnumerable<MethodNode> GetTypeMethods(TypeNode typeNode)
        {
            return OutEdges(typeNode)
                .Where(x => x.Tag is HasRelationship)
                .Where(x => x.Target is MethodNode)
                .Select(x => x.Target as MethodNode)!;
        }

        public IEnumerable<PropertyNode> GetTypeProperties(TypeNode typeNode)
        {
            return OutEdges(typeNode)
                .Where(x => x.Tag is HasRelationship)
                .Where(x => x.Target is PropertyNode)
                .Select(x => x.Target as PropertyNode)!;
        }

        public TypeNode GetOwningTypeOfMember(MemberNode memberNode)
        {
            return Edges
                .Where(x => x.Target.Equals(memberNode))
                .Where(x => x.Tag is HasRelationship)
                .Select(x => x.Source as TypeNode)
                .First()!;
        }

        public ProjectNode GetOwningProjectOfType(TypeNode typeNode)
        {
            return Edges
                .Where(x => x.Target.Equals(typeNode))
                .Where(x => x.Tag is BelongsToRelationship)
                .Select(x => x.Target as ProjectNode)
                .First()!;
        }

        public IEnumerable<TypeNode> GetTypesFromProject(ProjectNode projectNode)
        {
            return OutEdges(projectNode)
                .Where(x => x.Tag is BelongsToRelationship)
                .Select(x => x.Source as TypeNode)!;
        }

        public IEnumerable<MethodNode> ConstructedBy(TypeNode typeNode)
        {
            return Edges
                .Where(x => x.Target.Equals(typeNode))
                .Where(x => x.Tag is ConstructRelationship)
                .Select(x => x.Source as MethodNode)!;
        }

        public IEnumerable<TypeNode> Constructs(MethodNode methodNode)
        {
            return OutEdges(methodNode)
                .Where(x => x.Tag is ConstructRelationship)
                .Select(x => x.Target as TypeNode)!;
        }

        public IEnumerable<TypeNode> Declares(FileNode fileNode)
        {
            return Edges
                .Where(x => x.Target.Equals(fileNode))
                .Where(x => x.Tag is DeclaredAtRelationship)
                .Select(x => x.Source as TypeNode)!;
        }

        public FileNode DeclaredAt(TypeNode typeNode)
        {
            return OutEdges(typeNode)
                .Where(x => x.Tag is DeclaredAtRelationship)
                .Select(x => x.Target as FileNode)
                .First()!;
        }
    }

    public class TripleGraphService : ITripleGraph
    {
        private readonly ConcurrentDictionary<Node, Node> _nodes = new();
        private readonly ConcurrentDictionary<int, Node> _nodesByHashCode = new();
        private readonly TripleGraph _tripleGraph = new();

        public IEnumerable<MethodNode> GetTypeMethods(TypeNode typeNode)
        {
            TypeNode? node = _nodes[typeNode] as TypeNode;

            return _tripleGraph.GetTypeMethods(typeNode);
        }

        public IEnumerable<PropertyNode> GetTypeProperties(TypeNode typeNode)
        {
            TypeNode? node = _nodes[typeNode] as TypeNode;

            return _tripleGraph.GetTypeProperties(node!);
        }

        public TypeNode GetOwningTypeOfMember(MemberNode memberNode)
        {
            MemberNode? node = _nodes[memberNode] as MemberNode;

            return _tripleGraph.GetOwningTypeOfMember(node!);
        }

        public ProjectNode GetOwningProjectOfType(TypeNode typeNode)
        {
            TypeNode? node = _nodes[typeNode] as TypeNode;
            return _tripleGraph.GetOwningProjectOfType(node!);
        }

        public IEnumerable<TypeNode> GetTypesFromProject(ProjectNode projectNode)
        {
            ProjectNode? node = _nodes[projectNode] as ProjectNode;
            return _tripleGraph.GetTypesFromProject(node!);
        }

        public IEnumerable<MethodNode> ConstructedBy(TypeNode typeNode)
        {
            TypeNode? node = _nodes[typeNode] as TypeNode;
            return _tripleGraph.ConstructedBy(node!);
        }

        public IEnumerable<TypeNode> Constructs(MethodNode methodNode)
        {
            MethodNode? node = _nodes[methodNode] as MethodNode;
            return _tripleGraph.Constructs(node!);
        }

        public IEnumerable<TypeNode> Declares(FileNode fileNode)
        {
            FileNode? node = _nodes[fileNode] as FileNode;
            return _tripleGraph.Declares(fileNode!);
        }

        public FileNode DeclaredAt(TypeNode typeNode)
        {
            TypeNode? node = _nodes[typeNode] as TypeNode;
            return _tripleGraph.DeclaredAt(typeNode!);
        }

        public async Task SaveTriples(string fileName)
        {
            await CsvUtilities.WriteCsvAsync(fileName, _tripleGraph.Edges, new GraphClassMap());
        }

        public async Task LoadTriples(IEnumerable<Triple> triples)
        {
            foreach (Triple triple in triples.Distinct())
            {
                TryAddNode(triple.NodeA, out Node? nodeA);
                TryAddNode(triple.NodeB, out Node? nodeB);
                string relationshipType = triple.Relationship.Type;

                TaggedEdge<Node, Relationship> edge = new(nodeA!, nodeB!, triple.Relationship);

                _tripleGraph.AddVertex(nodeA!);
                _tripleGraph.AddVertex(nodeB!);
                _tripleGraph.AddEdge(edge);
            }

            await Console.Error.WriteLineAsync(
                $"Edge count: {_tripleGraph.EdgeCount} Vertex count: {_tripleGraph.VertexCount}");
            await Console.Error.WriteLineAsync($"{_nodes.Keys.Count}");
        }

        public bool ContainsNode(Node node)
        {
            return _nodes.ContainsKey(node) && _tripleGraph.ContainsVertex(node);
        }

        public void SetGraph(BaseTripleGraphAnalyzer baseTripleGraphAnalyzer)
        {
            baseTripleGraphAnalyzer.SetGraph(_tripleGraph);
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

                    Console.Error.WriteLine(
                        $"Node adding by HashCode: {node.GetHashCode()} - {node.FullName} - {node.GetType()}");
                    Console.Error.WriteLine(
                        $"Node collided by HashCode: {collideNode.GetHashCode()} - {collideNode.FullName} - {node.GetType()}");

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