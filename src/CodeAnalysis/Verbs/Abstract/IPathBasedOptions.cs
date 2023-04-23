using CommandLine;

namespace CodeAnalysis.Verbs.Abstract
{
    public interface IPathBasedOptions : IOptions
    {
        [Option('d', nameof(DirectoryPath), HelpText = "Path to directory structure containing project", Required = true)]
        string DirectoryPath { get; set; }
        
    }

    public interface IOptions
    {
        
    }
}