using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SilkHat.Utilities.Cli.Walkers
{
    public class CSharpVisualisationWalker : CSharpSyntaxWalker
    {
        private static int _tabs;

        public override void Visit(SyntaxNode node)
        {
            _tabs++;
            Console.WriteLine($"{new string('\t', _tabs)}{node.Kind()}: IsStatement={node is StatementSyntax}");
            base.Visit(node);
            _tabs--;
        }
    }
}