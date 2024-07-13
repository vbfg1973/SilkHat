// using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
//
// namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
// {
//     public class InvocationNode(MethodNode callingMethodNode, MethodNode targetMethodNode, int location)
//         : Node($"{callingMethodNode.FullName}->{targetMethodNode.FullName}",
//             $"{callingMethodNode.FullName}->{targetMethodNode.FullName}")
//     {
//         public int Location { get; } = location;
//
//         public override string Label { get; } = "Invocation";
//
//         public override string Set(string node)
//         {
//             return $"{base.Set(node)}, {node}.location = \"{Location}\"";
//         }
//     }
// }

