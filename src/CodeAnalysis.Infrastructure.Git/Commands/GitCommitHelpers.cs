namespace CodeAnalysis.Infrastructure.Git.Commands
{
    /// <summary>
    ///     Extension methods and helpers for dealing with git commit bodies
    /// </summary>
    public static class GitCommitHelpers
    {
        private const string CommitHeader = "commit";

        /// <summary>
        ///     Ensures path has a value. Either supplied value or current working directory
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CurrentWorkingDirectoryOrNominatedPath(string? path)
        {
            return string.IsNullOrWhiteSpace(path)
                ? "."
                : path;
        }

        /// <summary>
        ///     Tests if the current line is shaId commit header
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool IsCommitHeader(this string line)
        {
            return line.StartsWith(CommitHeader);
        }

        /// <summary>
        ///     Tests if the current line is a commit header other than commit shaId. Author, date, merge, etc
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool IsHeader(this string line)
        {
            if (line.Length <= 0 || !char.IsLetter(line[0])) return false;

            var seq = line.SkipWhile(ch => char.IsLetter(ch) && ch != ':');
            return seq.FirstOrDefault() == ':';
        }

        /// <summary>
        ///     Tests if the current line is part of a commit message body
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool IsMessageLine(this string line)
        {
            return line.Length > 0 && line[0] == '\t';
        }

        /// <summary>
        ///     Tests if the current line is a commit file status line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool IsFileStatusLine(this string line)
        {
            return line.Length > 1 && char.IsLetter(line[0]) && line[1] == '\t';
        }
    }
}