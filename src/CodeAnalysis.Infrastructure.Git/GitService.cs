using CodeAnalysis.Infrastructure.Git.Common;
using CodeAnalysis.Infrastructure.Git.Models;
using LibGit2Sharp;

namespace CodeAnalysis.Infrastructure.Git
{
    public interface IGitServiceFactory
    {
        IGitService CreateGitService(string repositoryPath);
    }

    public class GitServiceFactory : IGitServiceFactory
    {
        public IGitService CreateGitService(string repositoryPath)
        {
            return new GitService(repositoryPath);
        }
    }

    public interface IGitService
    {
        IEnumerable<CommitDetails> GetCommitDetails();
        CommitDetails GetCommitDetails(string shaId);
    }

    public class GitService : IGitService
    {
        private readonly Repository _repository;

        internal GitService(string repositoryPath)
        {
            _repository = new Repository(repositoryPath);
        }

        public IEnumerable<CommitDetails> GetCommitDetails()
        {
            return _repository.Commits.Select(commit => GitHelpers.MapCommitToDetails(_repository, commit));
        }

        public CommitDetails GetCommitDetails(string shaId)
        {
            return GitHelpers.MapCommitToDetails(_repository, _repository.Lookup<Commit>(shaId));
        }
    }
}