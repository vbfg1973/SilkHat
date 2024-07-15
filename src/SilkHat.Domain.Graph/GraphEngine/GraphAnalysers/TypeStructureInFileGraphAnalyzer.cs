using System.Text.Json;
using QuikGraph;
using SilkHat.Domain.Common.Locations;
using SilkHat.Domain.Graph.GraphEngine.Abstract;
using SilkHat.Domain.Graph.GraphEngine.GraphAnalysers.Models;
using SilkHat.Domain.Graph.SemanticTriples;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Relationships;

namespace SilkHat.Domain.Graph.GraphEngine.GraphAnalysers
{
    public class TypeStructureInFileGraphAnalyzer : BaseTripleGraphAnalyzer
    {
        public IEnumerable<TypeDefinition> TypeStructure(string fullPath)
        {
            FileNode? fileNode = AdjacencyGraph!
                .Vertices
                .OfType<FileNode>()
                .SingleOrDefault(x => x.FullName == fullPath);

            return fileNode != null ? TypeDefinitions(fileNode) : [];
        }

        private IEnumerable<TypeDefinition> TypeDefinitions(FileNode fileNode)
        {
            var typesDeclaredIn = AdjacencyGraph!.Edges.Where(x => x.Target.Equals(fileNode)).Select(x => x.Source as TypeNode).ToList();
            
            foreach (var declaredType in typesDeclaredIn.Where(x => x != null))
            {
                var methods = GetTypeMethods(declaredType!).Select(x =>
                {
                    var locationNode = GetLocationNodes(x).First();
                    return new NodeWithLocation<MethodNode>(x, locationNode);
                }).ToList();
                
                var properties = GetTypeProperties(declaredType!).Select(x =>
                {
                    var locationNode = GetLocationNodes(x).First();
                    return new NodeWithLocation<PropertyNode>(x, locationNode);
                }).ToList();
                
                var td = new TypeDefinition(
                    declaredType!, 
                    methods,
                    properties);

                Console.WriteLine(JsonSerializer.Serialize(td));

                yield return td;
            }
        }

        private IEnumerable<LocationNode> GetLocationNodes(CodeNode codeNode)
        {
            return AdjacencyGraph!.Edges
                .Where(x => x.Source.Equals(codeNode) && x.Tag is HasLocationRelationship)
                .Select(x => x.Target as LocationNode)!;  
        }
        
        private IEnumerable<PropertyNode> GetTypeProperties(TypeNode typeNode)
        {
            return AdjacencyGraph!.Edges
                .Where(x => x.Source.Equals(typeNode) && x.Tag is HasRelationship && x.Target is PropertyNode)
                .Select(x => x.Target as PropertyNode)!;            
        }
        
        private IEnumerable<MethodNode> GetTypeMethods(TypeNode typeNode)
        {
            return AdjacencyGraph!.Edges
                .Where(x => x.Source.Equals(typeNode) && x.Tag is HasRelationship && x.Target is MethodNode)
                .Select(x => x.Target as MethodNode)!;
        }
    }
}