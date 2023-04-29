using System.Text.Json;
using CodeAnalysis.Infrastructure.Git;
using CommandLine;

namespace CodeAnalysis.Verbs.Git
{
    [Verb("git", HelpText = "Testing sucking data out of git repos")]
    public class GitOptions
    {
        [Option('p', nameof(RepositoryPath), HelpText = "Path to git repository")]
        public string RepositoryPath { get; set; } = null!;
    }

    public class GitVerb
    {
        private readonly IGitServiceFactory _gitServiceFactory;

        public GitVerb(IGitServiceFactory gitServiceFactory)
        {
            _gitServiceFactory = gitServiceFactory;
        }

        public async Task Run(GitOptions options)
        {
            var gitService = _gitServiceFactory.CreateGitService(options.RepositoryPath);

            foreach (var commitDetails in gitService.GetCommitDetails())
                Console.WriteLine(JsonSerializer.Serialize(commitDetails));
        }
    }
}