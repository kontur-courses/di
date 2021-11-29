using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Preprocessing;

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
        public void ParseWords_WithNull_ThrowsException() =>
            Assert.That(() => parser.ParseWords(null), Throws.InstanceOf<ArgumentException>());

        [TestCaseSource(nameof(ParseWordsReturnWordInfosCases))]
        public void ParseWords_WithNormalizedWords_ReturnWordInfos(List<string> words, List<WordInfo> expected)
        {
            parser.ParseWords(words)
                .Should().BeEquivalentTo(expected);
        }

        private static IEnumerable<TestCaseData> ParseWordsReturnWordInfosCases()
        {
            yield return new TestCaseData(
                new List<string> {"привет", "в", "этот", "чудесный"},
                new List<WordInfo>
                {
                    new("привет", SpeechPart.S),
                    new("в", SpeechPart.PR),
                    new("этот", SpeechPart.APRO),
                    new("чудесный", SpeechPart.A),
                }) {TestName = "Normalized words"};

            yield return new TestCaseData(
                    new List<string> {"приветик", "чудеснейший"},
                    new List<WordInfo> {new("приветик", SpeechPart.S), new("чудеснейший", SpeechPart.A),})
                {TestName = "Words in different forms"};
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
        [TestCase("12", TestName = "Number")]
        [TestCase("hello", TestName = "Eng word")]
        [TestCase("!", TestName = "Sign")]
        public void ParseWord_ThrowsException(string input) =>
            Assert.That(() => parser.ParseWords(new[] {input}), Throws.InstanceOf<ApplicationException>());
    }
}