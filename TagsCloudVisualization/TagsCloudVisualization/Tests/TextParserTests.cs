using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Logic;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TextParserTests
    {
        private TextParser textParser;

        [OneTimeSetUp]
        public void InitializeTextParser()
        {
            textParser = new TextParser();
        }

        [Test]
        public void TextParser_ThrowsArgumentNullException_WhenTextIsNull()
        {
            Action action = () => textParser.ParseToTokens(null, null).ToArray();
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void TextParser_ReturnsEmptyCollection_WhenTextIsEmpty()
        {
            var text = "";
            textParser.ParseToTokens(text, null).Should().BeEmpty();
        }

        [Test]
        public void TextParser_ReturnsCorrectResult_WithRegularTags()
        {
            var wordTags = new[] {"dog", "cat", "rat", "monkey" };
            var text = wordTags.Aggregate("", (current, word) => current + (word + Environment.NewLine));
            textParser
                .ParseToTokens(text, null)
                .Select(token => token.Word)
                .ToArray()
                .Should()
                .BeEquivalentTo(wordTags);
        }

        [Test]
        public void TextParser_ReturnsCorrectResult_WithEmptyLines()
        {
            var wordTags = new[] { "dog", "cat", "", "monkey", ""};
            var text = wordTags.Aggregate("", (current, word) => current + (word + Environment.NewLine));
            textParser
                .ParseToTokens(text, null)
                .Select(token => token.Word)
                .ToArray()
                .Should()
                .BeEquivalentTo(
                    wordTags
                        .Where(word => !string.IsNullOrEmpty(word))
                    );
        }

        [Test]
        public void TextParser_ReturnsCorrectResult_WhenWordRepeatsManyTimes()
        {
            var wordTags = new[] { "dog", "dog", "dog", "dog"};
            var text = wordTags.Aggregate("", (current, word) => current + (word + Environment.NewLine));
            var tokens = textParser.ParseToTokens(text, null).ToArray();
            tokens.Length.Should().Be(1);
            tokens.First().Should().BeEquivalentTo(new WordToken("dog", 4));
        }

        [Test]
        public void TextParser_ReturnsEmptyCollection_WhenWordsAreBoring()
        {
            var wordTags = new[] {"i", "am", "not"};
            var boringWords = new HashSet<string>(wordTags);
            var text = wordTags.Aggregate("", (current, word) => current + (word + Environment.NewLine));
            textParser.ParseToTokens(text, boringWords).Should().BeEmpty();
        }
    }
}