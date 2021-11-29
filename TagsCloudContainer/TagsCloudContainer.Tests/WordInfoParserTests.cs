using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.Tests
{
    public class WordInfoParserTests
    {
        private WordInfoParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new WordInfoParser();
        }

        [Test]
        public void ParseWords_WithNormalizedWords_ReturnWordInfos()
        {
            var words = new List<string> {"привет", "в", "этот", "чудесный"};
            var expected = new List<WordInfo>
            {
                new("привет", SpeechPart.S),
                new("в", SpeechPart.PR),
                new("этот", SpeechPart.APRO),
                new("чудесный", SpeechPart.A),
            };

            parser.ParseWords(words)
                .Should().BeEquivalentTo(expected);
        }

        [Timeout(500)]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void ParseWord_IgnoreString(string input)
        {
            var words = new List<string> {input, "мир"};
            parser.ParseWords(words)
                .Should().BeEquivalentTo(new List<WordInfo> {new("мир", SpeechPart.S)});
        }

        [Timeout(500)]
        [TestCase("12", TestName = "Not word")]
        [TestCase("hello", TestName = "Eng word")]
        public void ParseWord_ThrowsException(string input) =>
            Assert.That(() => parser.ParseWords(new[] {input}), Throws.InstanceOf<ApplicationException>());
    }
}