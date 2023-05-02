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
            return RegexIsHeaderLine.IsMatch(line);
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
        /// <returns></returns>
        public static bool IsFileStatusLine(this string line)
        {
            // Incorrect:
            // Does not account for "R100" lines (indicating a rename with a 100% to the original
            // return line.Length > 1 && char.IsLetter(line[0]) && line[1] == '\t';

            return RegexIsFileStatus.IsMatch(line);
        }

        /// <summary>
        ///     Returns a ChangeKind enum based on the character parsed from a fileStatus line in git log output
        /// </summary>
        /// <param name="fileStatus"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static ChangeKind MapStatusToChangeKind(char fileStatus)
        {
            return fileStatus switch
            {
                'A' => ChangeKind.Added,
                'D' => ChangeKind.Deleted,
                'M' => ChangeKind.Modified,
                'R' => ChangeKind.Renamed,
                'I' => ChangeKind.Ignored,
                'T' => ChangeKind.TypeChanged,
                _ => throw new ArgumentOutOfRangeException(nameof(fileStatus), $"Unknown fileStatus: {fileStatus}")
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