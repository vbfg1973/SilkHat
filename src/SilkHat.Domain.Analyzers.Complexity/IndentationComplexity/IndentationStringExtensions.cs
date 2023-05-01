namespace SilkHat.Domain.Analyzers.Complexity.IndentationComplexity
{
    /// <summary>
    ///     Helpers for indentation complexity analysis
    /// </summary>
    public static class IndentationStringExtensions
    {
        /// <summary>
        ///     Counts the leading whitespace of a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns>
        ///     An integer representing the count of the leading whitespace. The type of whitespace is not taken into
        ///     account so tabs, spaces, etc are considered equivalent.
        /// </returns>
        /// <remarks>
        ///     Counts the leading whitespace of a string, favouring for loops over TakeWhile Linq methods to
        ///     reduce allocations and optimise for speed. Don't change this to LINQ for the sake of readability!
        /// </remarks>
        public static int LeadingWhitespaceCount(this string str)
        {
            var count = 0;
            for (var i = 0; i <= str.Length; i++)
            {
                if (!char.IsWhiteSpace(str[i])) break;

                count++;
            }

            return count;
        }

        /// <summary>
        ///     Counts from the start of a string to the point where 'c' is not seen.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private static int LeadingCharacterCount(this string str, char c)
        {
            var count = 0;
            for (var i = 0; i <= str.Length; i++)
            {
                if (str[i] != c) break;

                count++;
            }

            return count;
        }

        /// <summary>
        ///     Tests if leading character of string is whitespace
        /// </summary>
        /// <param name="str"></param>
        /// <param name="firstWhiteSpaceCharacter"></param>
        /// <returns></returns>
        public static bool IsLedByWhiteSpace(this string str, out char firstWhiteSpaceCharacter)
        {
            if (char.IsWhiteSpace(str[0]))
            {
                firstWhiteSpaceCharacter = str[0];
                return true;
            }

            firstWhiteSpaceCharacter = '\0';
            return false;
        }

        /// <summary>
        ///     Tests if a string is entirely composed of whitespace
        /// </summary>
        /// <param name="str"></param>
        /// <returns>false if a string contains any non-whitespace characters, otherwise returns true</returns>
        public static bool IsAllWhiteSpace(this string str)
        {
            var length = str.Length;
            for (var i = 0; i < length; i++)
                if (!char.IsWhiteSpace(str[i]) && i < length - 1)
                    return false;

            return true;
        }

        internal static string[] CleanLinesArray(this IEnumerable<string> lines)
        {
            return lines
                .Select(str => str.TrimEnd())
                .Where(str => !string.IsNullOrEmpty(str))
                .Where(str => !string.IsNullOrWhiteSpace(str))
                .ToArray();
        }
    }
}