using CommandLine;

namespace CodeAnalysis.Verbs.Abstract
{
    public interface ISolutionOptions : IOptions
    {
        [Option('s', nameof(SolutionPath), HelpText = "Path to solution file", Required = true)]
        string SolutionPath { get; set; }
        
        [Option('c', nameof(CompileSolution), HelpText = "Compile the solution when loaded", Default = false)]
        bool CompileSolution { get; set; }
    }

    public interface IOptions
    {
        
    }
}