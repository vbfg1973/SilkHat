using System.Text.RegularExpressions;
using SilkHat.Infrastructure.Git.Commands.Commits.CommitDetails;

namespace SilkHat.Infrastructure.Git.Commands.Commits
{
    /// <summary>
    ///     Extension methods and helpers for dealing with git commit bodies
    /// </summary>
    public static partial class GitCommitHelpers
    {
        private const string CommitHeader = "commit";
        private const int MessageSpaceIndent = 4;
        private static readonly Regex RegexIsFileStatus = GenerateFileStatusMatchLineRegex();
        private static readonly Regex RegexIsMessageLine = GenerateMessageMatchLineRegex();
        private static readonly Regex RegexIsHeaderLine = GenerateHeaderMatchLineRegex();

        /// <summary>
        ///     Force line endings to be replaces by a newline, irrespective of the original type
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NormaliseLineEndings(this string str)
        {
            return str.ReplaceLineEndings('\n'.ToString());
        }

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
        public static bool TryParseCommitHeader(this string line, out string sha)
        {
            sha = string.Empty;
            if (!line.StartsWith(CommitHeader)) return false;

            sha = line.Split(' ')[1];
            return true;
        }

        /// <summary>
        ///     Tests if the current line is a commit header other than commit shaId. Author, date, merge, etc
        /// </summary>
        /// <param name="line"></param>
        /// <param name="headerName"></param>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        public static bool TryParseHeader(this string line, out string headerName, out string headerValue)
        {
            headerName = string.Empty;
            headerValue = string.Empty;

            if (!RegexIsHeaderLine.IsMatch(line)) return false;

            var elements = line.Split(':');
            headerName = elements[0];
            headerValue = string.Join(':', elements.Skip(1)).Trim();

            return true;
        }

        /// <summary>
        ///     Tests if the current line is part of a commit message body
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool IsMessageLine(this string line)
        {
            return RegexIsMessageLine.IsMatch(line);
        }

        /// <summary>
        ///     Tests if the current line is a commit file status line
        /// </summary>
        /// <param name="line"></param>
        /// <param name="changeKind"></param>
        /// <param name="currentPath"></param>
        /// <param name="oldPath"></param>
        /// <returns></returns>
        public static bool TryParseFileStatusLine(this string line, out ChangeKind changeKind, out string currentPath,
            out string oldPath)
        {
            changeKind = ChangeKind.Unmodified;
            currentPath = string.Empty;
            oldPath = string.Empty;

            if (!RegexIsFileStatus.IsMatch(line)) return false;

            changeKind = MapStatusToChangeKind(line);

            // The last two (of a possible three) tab seperated elements are the paths.
            // If there's only one then 
            var pathElements = line.Split('\t').Skip(1).ToArray();

            switch (pathElements.Length)
            {
                case 1:
                    currentPath = pathElements[0];
                    oldPath = pathElements[0];
                    break;
                case 2:
                    oldPath = pathElements[0];
                    currentPath = pathElements[1];
                    break;
                default:
                    throw new Exception();
            }

            return true;
        }

        /// <summary>
        ///     Returns a ChangeKind enum based on the character parsed from a fileStatus line in git log output
        /// </summary>
        /// <param name="fileStatus"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static ChangeKind MapStatusToChangeKind(string fileStatusLine)
        {
            var c = fileStatusLine[0];
            return c switch
            {
                'A' => ChangeKind.Added,
                'D' => ChangeKind.Deleted,
                'M' => ChangeKind.Modified,
                'R' => ChangeKind.Renamed,
                'I' => ChangeKind.Ignored,
                'T' => ChangeKind.TypeChanged,
                _ => throw new ArgumentOutOfRangeException(nameof(fileStatusLine),
                    $"Unknown fileStatus: {fileStatusLine}")
            };
        }

        /// <summary>
        ///     Generate a regex to match fileStatus lines in git log output
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex("^[A-Z]\\w*\\s")]
        private static partial Regex GenerateFileStatusMatchLineRegex();

        [GeneratedRegex(@"^(?:\t|\s{4})")] // Leads with a tab or four spaces
        private static partial Regex GenerateMessageMatchLineRegex();

        [GeneratedRegex(@"^\w+\:")] // is a header line
        private static partial Regex GenerateHeaderMatchLineRegex();

        [GeneratedRegex(@"^commit\s")] // is a header line
        private static partial Regex GenerateCommitMatchLineRegex();
    }
}