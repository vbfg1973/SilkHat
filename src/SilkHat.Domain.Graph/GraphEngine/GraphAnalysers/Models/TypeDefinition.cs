using SilkHat.Domain.Common.Locations;
using SilkHat.Domain.Graph.SemanticTriples;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.GraphEngine.GraphAnalysers.Models
{
    public class FileTypes(FileNode file, IEnumerable<TypeDefinition> types)
    {
        public FileNode File { get; } = file;

        public List<TypeDefinition> Types { get; set; } = types.ToList();
    }
    
    public class TypeDefinition(TypeNode typeNode, IEnumerable<NodeWithLocation<MethodNode>> methods, IEnumerable<NodeWithLocation<PropertyNode>> properties)
    {
        public TypeNode Type { get; } = typeNode;
        public List<NodeWithLocation<MethodNode>> Methods { get; } = methods.ToList();
        public List<NodeWithLocation<PropertyNode>> Properties { get; } = properties.ToList();
    }

    public class NodeWithLocation<T>(T node, LocationNode location)
        where T : Node
    {
        public T Node { get; } = node;
        public LocationNode Location { get; } = location;
    }
}