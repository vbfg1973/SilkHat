// using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
//
// namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
// {
//     public class PackageNode(string fullName, string name, string version) : Node(fullName, name)
//     {
//         public override string Label { get; } = "Package";
//
//         public string Version { get; } = version;
//
//         public override string Set(string node)
//         {
//             return $"{base.Set(node)}, {node}.version = \"{Version}\"";
//         }
//     }
// }

