using Microsoft.CodeAnalysis.CSharp;
using SilkHat.Utilities.Cli.Walkers;

namespace SilkHat.Utilities.Cli
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