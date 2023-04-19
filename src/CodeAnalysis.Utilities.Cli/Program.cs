using CodeAnalysis.Utilities.Cli.Walkers;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeAnalysis.Utilities.Cli
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(args[0]));

            var walker = new CSharpVisualisationWalker();
            walker.Visit(tree.GetRoot());
        }
    }
}