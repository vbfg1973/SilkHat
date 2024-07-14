﻿using System.Text.RegularExpressions;

namespace SilkHat.Domain.Common
{
    public static partial class StringExtensions
    {
        private static readonly HashSet<string> _stopWords = new()
        {
            "call", "upon", "still", "nevertheless", "down", "every", "forty", "‘re", "always", "whole", "side",
            "nt", "now", "however", "an", "show", "least", "give", "below", "did", "sometimes", "which", "s",
            "nowhere", "per", "hereupon", "yours", "she", "moreover", "eight", "somewhere", "within", "whereby", "few",
            "has", "so", "have", "for", "noone", "top", "were", "those", "thence", "eleven", "after", "no", "ll",
            "others", "ourselves", "themselves", "though", "that", "nor", "just", "s", "before", "had", "toward",
            "another", "should", "herself", "and", "these", "such", "elsewhere", "further", "next", "indeed", "bottom",
            "anyone", "his", "each", "then", "both", "became", "third", "whom", "‘ve", "mine", "take", "many",
            "anywhere", "to", "well", "thereafter", "besides", "almost", "front", "fifteen", "towards", "none", "be",
            "herein", "two", "using", "whatever", "please", "perhaps", "full", "ca", "we", "latterly", "here",
            "therefore", "us", "how", "was", "made", "the", "or", "may", "re", "namely", "ve", "anyway", "amongst",
            "used", "ever", "of", "there", "than", "why", "really", "whither", "in", "only", "wherein", "last", "under",
            "own", "therein", "go", "seems", "‘m", "wherever", "either", "someone", "up", "doing", "on", "rather",
            "ours", "again", "same", "over", "‘s", "latter", "during", "done", "re", "put", "m", "much", "neither",
            "among", "seemed", "into", "once", "my", "otherwise", "part", "everywhere", "never", "myself", "must",
            "will", "am", "can", "else", "although", "as", "beyond", "are", "too", "becomes", "does", "a", "everyone",
            "but", "some", "regarding", "‘ll", "against", "throughout", "yourselves", "him", "d", "it", "himself",
            "whether", "move", "m", "hereafter", "re", "while", "whoever", "your", "first", "amount", "twelve",
            "serious", "other", "any", "off", "seeming", "four", "itself", "nothing", "beforehand", "make", "out",
            "very", "already", "various", "until", "hers", "they", "not", "them", "where", "would", "since",
            "everything", "at", "together", "yet", "more", "six", "back", "with", "thereupon", "becoming", "around", "due", 
            "keep", "somehow", "n‘t", "across", "all", "when", "i", "empty", "nine", "five", "get", "see", "been", "name",
            "between", "hence", "ten", "several", "from", "whereupon", "through", "hereby", "ll", "alone", "something",
            "formerly", "without", "above", "onto", "except", "enough", "become", "behind", "d", "its", "most", "nt",
            "might", "whereas", "anything", "if", "her", "via", "fifty", "is", "thereby", "twenty", "often",
            "whereafter", "their", "also", "anyhow", "cannot", "our", "could", "because", "who", "beside", "by",
            "whence", "being", "meanwhile", "this", "afterwards", "whenever", "mostly", "what", "one", "nobody", "seem",
            "less", "do", "‘d", "say", "thus", "unless", "along", "yourself", "former", "thru", "he", "hundred",
            "three", "sixty", "me", "sometime", "whose", "you", "quite", "ve", "about", "even"
        };

        /// <summary>
        ///     Splits string on capitals. Will honour consecutive capitals such as "USA" as full
        ///     words and not split on those
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IEnumerable<string> SplitStringOnCapitals(this string str)
        {
            Regex regex = SplitStringRegex();
            return regex.Replace(str, "---")
                .Split("---", StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.ToLowerInvariant())
                .Where(word => !_stopWords.Contains(word));
        }

        [GeneratedRegex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace)]
        private static partial Regex SplitStringRegex();
    }
}