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

            return fileNode != null 
                ? TypeDefinitions(fileNode) 
                : [];
        }

        private IEnumerable<TypeDefinition> TypeDefinitions(FileNode fileNode)
        {
            List<TypeNode?> typesDeclaredInFile = GetTypesDeclaredInFile(fileNode)
                .ToList();

            foreach (TypeNode? declaredType in typesDeclaredInFile.Where(x => x != null))
            {
                yield return new TypeDefinition(
                    declaredType!,
                    GetMethodsFromTypeWithLocations(declaredType),
                    GetPropertiesFromTypeWithLocations(declaredType));
            }
        }

        private IEnumerable<TypeNode?> GetTypesDeclaredInFile(FileNode fileNode)
        {
            return AdjacencyGraph!
                .Edges
                .Where(x => x.Target.Equals(fileNode))
                .Select(x => x.Source as TypeNode);
        }

        private IEnumerable<NodeWithLocation<PropertyNode>> GetPropertiesFromTypeWithLocations(TypeNode? declaredType)
        {
            return GetTypeProperties(declaredType!).Select(x =>
            {
                LocationNode locationNode = GetLocationNodes(x).First();
                return new NodeWithLocation<PropertyNode>(x, locationNode);
            });
        }

        private IEnumerable<NodeWithLocation<MethodNode>> GetMethodsFromTypeWithLocations(TypeNode? declaredType)
        {
            return GetTypeMethods(declaredType!).Select(x =>
            {
                LocationNode locationNode = GetLocationNodes(x).First();
                return new NodeWithLocation<MethodNode>(x, locationNode);
            });
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