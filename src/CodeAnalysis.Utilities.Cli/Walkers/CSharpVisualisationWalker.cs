using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysis.Utilities.Cli.Walkers
{
    public class CSharpVisualisationWalker : CSharpSyntaxWalker
    {
        private static int _tabs = 0;

        public override void Visit(SyntaxNode node)
        {
            _tabs++;
            var indentation = new string('\t', _tabs);
            Console.WriteLine($"{indentation}{node.Kind()}: {node is StatementSyntax}");
            base.Visit(node);
            _tabs--;
        }
    }
}