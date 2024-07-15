using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.AST
{
    public interface ISyntaxStructureBuilder
    {
        SyntaxStructure? SyntaxStructure(string sourceText);
    }

    public class SyntaxStructureBuilder : ISyntaxStructureBuilder
    {
        public SyntaxStructure? SyntaxStructure(string sourceText)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceText);
            tree.TryGetRoot(out var root);
            
            SyntaxStructure nodeStructure = MapSyntaxStructure(root!);

            foreach (SyntaxNode child in root!.ChildNodes())
            {
                BuildStructure(nodeStructure, child);
            }

            return nodeStructure;
        }

        private static void BuildStructure(SyntaxStructure parentStructure, SyntaxNode node)
        {
            SyntaxStructure nodeStructure = MapSyntaxStructure(node);

            foreach (SyntaxNode child in node.ChildNodes())
            {
                BuildStructure(nodeStructure, child);
            }

            parentStructure.Children.Add(nodeStructure);
        }

        private static SyntaxStructure MapSyntaxStructure(SyntaxNode syntaxNode)
        {
            return new SyntaxStructure(syntaxNode.GetType().Name, new List<SyntaxStructure>());
        }
    }
}