using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using TagCloud.TextParsing;

namespace TagCloudUnitTests
{
    [TestFixture]
    internal class TextParserTests
    {
        private TextParser parser;

        private List<string> exprectedWords = new List<string>() { "One", "two", "three", "four", "five", "six" };

        [SetUp]
        public void Setup()
        {
            parser = new TextParser();
        }

        [Test]
        public void GetWords_ReturnsWords_WhenTextWithoutPunctuationMarks()
        {
            var text = "One two three four five six";

            var actualWords = parser.GetWords(text);

            actualWords.Should().BeEquivalentTo(exprectedWords);
        }

        [Test]
        public void GetWords_ReturnsWords_WhenTextWithPunctuationMarks()
        {
            var text = "One, two: three. four; five! - six?";

            var actualWords = parser.GetWords(text);

            actualWords.Should().BeEquivalentTo(exprectedWords);
        }

        [TestCase("One\ntwo\nthree\nfour\nfive\nsix\n", TestName = "Only new line characters")]
        [TestCase("One\r\ntwo\r\nthree\r\nfour\r\nfive\r\nsix\r\n", TestName = "New line and carriage return characters")]
        public void GetWords_ReturnsWords_WhenTextWithNewLineCharacters(string text)
        {
            var actualWords = parser.GetWords(text);

            actualWords.Should().BeEquivalentTo(exprectedWords);
        }
    }
}
