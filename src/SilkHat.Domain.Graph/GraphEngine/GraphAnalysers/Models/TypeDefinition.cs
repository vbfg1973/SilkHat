using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.Domain.Graph.GraphEngine.GraphAnalysers.Models
{
    public class FileTypes(FileNode file, IEnumerable<TypeDefinition> types)
    {
        public FileNode File { get; } = file;

        public List<TypeDefinition> Types { get; set; } = types.ToList();
    }
    
    public class TypeDefinition(TypeNode typeNode, IEnumerable<MethodNode> methods, IEnumerable<PropertyNode> properties)
    {
        public TypeNode Type { get; } = typeNode;
        public List<MethodNode> Methods { get; } = methods.ToList();
        public List<PropertyNode> Properties { get; } = properties.ToList();
    }
}