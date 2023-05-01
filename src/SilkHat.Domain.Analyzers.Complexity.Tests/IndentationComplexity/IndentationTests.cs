using SilkHat.Domain.Analyzers.Complexity.IndentationComplexity;
using FluentAssertions;
using SilkHat.Domain.Analyzers.Complexity.Tests.IndentationComplexity.ClassData;

namespace SilkHat.Domain.Analyzers.Complexity.Tests.IndentationComplexity
{
    public class IndentationTests
    {
        [Theory]
        [InlineData("String contents", '\t', 5)]
        [InlineData("String contents", '\t', 15)]
        [InlineData("String contents", ' ', 15)]
        [InlineData("String contents", ' ', 5)]
        public void GivenStringWithIndentationCountsIndentationCorrectly(string str, char whiteSpaceCharacter,
            int expected)
        {
            if (!char.IsWhiteSpace(whiteSpaceCharacter))
                throw new ArgumentException("Not a whitespace character", nameof(whiteSpaceCharacter));

            var testString = string.Concat(new string(whiteSpaceCharacter, expected), str);

            testString
                .LeadingWhitespaceCount()
                .Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("String contents", 0)]
        [InlineData("Another string", 0)]
        public void GivenStringWithNonWhiteSpaceIndentationCountsCorrectly(string testString, int expected)
        {
            testString
                .LeadingWhitespaceCount()
                .Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("\\tSomeString", '\t')]
        [InlineData(" SomeString", ' ')]
        public void GivenStringWithLeadingWhiteSpaceCorrectlyIdentifiesWsCharacter(string testString,
            char expectedWhitespaceCharacter)
        {
            if (testString.IsLedByWhiteSpace(out var wsCharacter))
                wsCharacter
                    .Should()
                    .Be(expectedWhitespaceCharacter);
        }

        [Theory]
        [InlineData("SomeString")]
        [InlineData("AnotherString")]
        public void GivenStringWithoutLeadingWhiteSpaceShouldNotIdentify(string testString)
        {
            testString.IsLedByWhiteSpace(out _)
                .Should()
                .BeFalse();
        }

        [Theory]
        [InlineData("HasNoWhitespace")]
        [InlineData("   HasLeadingWhitespace")]
        [InlineData("HasTrailingWhitespace   ")]
        [InlineData("\\t\\t\\tHasLeadingWhitespace")]
        [InlineData("HasTrailingWhitespace\\t\\t\\t")]
        public void GivenStringWithNonWhiteSpaceCharactersIsFalse(string testString)
        {
            testString
                .IsAllWhiteSpace()
                .Should()
                .BeFalse();
        }
        
        [Theory]
        [ClassData(typeof(MixedAllWhiteSpaceStrings))]
        public void GivenStringWithAllWhiteSpaceCharactersIsTrue(string testString)
        {
            testString
                .IsAllWhiteSpace()
                .Should()
                .BeTrue();
        }
    }
}