using System.Text;
using CodeAnalysis.Infrastructure.Git.Models;
using LibGit2Sharp;

namespace CodeAnalysis.Infrastructure.Git.Common
{
    /// <summary>
    ///     Helpers for git
    /// </summary>
    internal static class GitHelpers
    {
        /// <summary>
        ///     Map a commit from a repository to a full CommitDetails object
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        public static CommitDetails MapCommitToDetails(Repository repository, Commit commit)
        {
            var commitFileSet = new CommitFileSet();

            foreach (var file in
                     GetPathsFromCommit(repository, commit).Select(path => new CommitFile(path)))
                commitFileSet.Add(file);

            return new CommitDetails(
                commit.Sha,
                commit.Author.When,
                new CommitAuthor(
                    commit.Author.Name,
                    commit.Author.Email),
                commitFileSet);
        }

        /// <summary>
        ///     Get all commit ids
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        internal static IEnumerable<string> GetCommitIds(Repository repository)
        {
            return repository.Commits.Select(commit => commit.Sha);
        }

        /// <summary>
        ///     Attempts to get the commit identified by the shaId from the nominated repository
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="shaId"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        internal static bool TryGetCommit(Repository repository, string shaId, out Commit commit)
        {
            commit = repository.Lookup<Commit>(shaId);

            return commit != null;
        }

        /// <summary>
        ///     Attempts to retrieve the patch from the commit by comparing it to its parent
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="commit"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        internal static bool TryGetPatch(Repository repository, Commit commit, out Patch patch)
        {
            patch = null!;

            var newTree = commit.Tree;
            var oldTree = commit.Parents.FirstOrDefault()?.Tree;

            if (oldTree == null) return false;

            patch = repository.Diff.Compare<Patch>(oldTree, newTree);

            return true;
        }

        /// <summary>
        ///     Attempts to retrieve the contents of the nominated file from the commit
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="commit"></param>
        /// <param name="filePath"></param>
        /// <param name="fileContents"></param>
        /// <returns></returns>
        internal static bool TryGetFileFromCommit(Repository repository, Commit commit, string filePath,
            out string fileContents)
        {
            fileContents = string.Empty;

            if (!TryGetPatch(repository, commit, out var patch)) return false;

            // Find the change for the nominated path
            var patchEntryChanges = patch.SingleOrDefault(patchEntryChanges => patchEntryChanges.Path == filePath);
            if (patchEntryChanges == null) return false;

            // Get the blob for the path from the change
            var treeEntry = commit[patchEntryChanges.Path];
            if (treeEntry == null) return false;
            if (treeEntry.TargetType != TreeEntryTargetType.Blob) return false;

            // Get the file contents and signal success
            fileContents = GetFileContentAsync(treeEntry).Result;
            return true;
        }

        internal static IEnumerable<string> GetPathsFromCommit(Repository repository, Commit commit)
        {
            if (!TryGetPatch(repository, commit, out var patch)) yield break;

            foreach (var patchChange in patch) yield return patchChange.Path;
        }

        /// <summary>
        ///     Gets the contents of a file from a blob TreeEntry
        /// </summary>
        /// <param name="treeEntry"></param>
        /// <returns></returns>
        private static async Task<string> GetFileContentAsync(TreeEntry treeEntry)
        {
            var blob = (Blob)treeEntry.Target;
            var contentStream = blob.GetContentStream();
            using var streamReader = new StreamReader(contentStream, Encoding.UTF8);
            return await streamReader.ReadToEndAsync();
        }
    }
}