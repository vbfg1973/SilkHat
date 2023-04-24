namespace CodeAnalysis.Domain.Analyzers.Complexity.IndentationComplexity
{
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
        ///     Tests if all leading whitespace is of the same type
        /// </summary>
        /// <param name="str"></param>
        /// <param name="whiteSpaceCharacter"></param>
        /// <returns>
        /// true if all whitespace is the same
        /// false if it differs
        /// false if no leading whitespace
        /// </returns>
        public static bool LeadingWhiteSpaceConformity(this string str)
        {
            if (!str.IsLedByWhiteSpace(out var firstWhiteSpaceCharacter)) return false;

            var wsCount = str.LeadingWhitespaceCount();
            var leadingCount = str.LeadingCharacterCount(firstWhiteSpaceCharacter);

            return wsCount == leadingCount;
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
        private static bool IsLedByWhiteSpace(this string str, out char firstWhiteSpaceCharacter)
        {
            if (char.IsWhiteSpace(str[0]))
            {
                firstWhiteSpaceCharacter = str[0];
                return true;
            }

            firstWhiteSpaceCharacter = '\0';
            return false;
        }
    }
}