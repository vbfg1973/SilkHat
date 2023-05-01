using CommandLine;

namespace SilkHat.Verbs.Abstract
{
    public interface IPathBasedOptions : IOptions
    {
        [Option('d', nameof(DirectoryPath), HelpText = "Path to directory structure containing project",
            Required = true)]
        string DirectoryPath { get; set; }

        [Option('o', nameof(OutputCsv), HelpText = "Path to output csv", Required = true)]
        string OutputCsv { get; set; }
    }

    public interface IOptions
    {
    }
}