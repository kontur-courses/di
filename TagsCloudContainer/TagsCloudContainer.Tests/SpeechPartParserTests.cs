using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Tests
{
    public class SpeechPartParserParserTests
    {
        private WordSpeechPartParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new WordSpeechPartParser();
        }

        [Test]
        public void ParseWords_WithNull_ThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => parser.ParseWords(null));

        [Timeout(500)]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void ParseWord_IgnoreString(string input)
        {
            var words = new List<string> {input, "мир"};
            parser.ParseWords(words)
                .Should().BeEquivalentTo(new List<SpeechPartWord> {new("мир", SpeechPart.S)});
        }

        [Timeout(500)]
        [TestCase("12", TestName = "Number")]
        [TestCase("hello", TestName = "Eng word")]
        [TestCase("!", TestName = "Sign")]
        public void ParseWord_ThrowsException(string input) =>
            Assert.Throws<ApplicationException>(() => parser.ParseWords(new[] {input}));

        [TestCaseSource(nameof(ParseWordsReturnWordInfosCases))]
        public void ParseWords_WithNormalizedWords_ReturnWordInfos(List<string> words,
            List<SpeechPartWord> expected)
        {
            parser.ParseWords(words)
                .Should().BeEquivalentTo(expected);
        }

        private static IEnumerable<TestCaseData> ParseWordsReturnWordInfosCases()
        {
            yield return new TestCaseData(
                new List<string> {"привет", "в", "этот", "чудесный"},
                new List<SpeechPartWord>
                {
                    new("привет", SpeechPart.S),
                    new("в", SpeechPart.PR),
                    new("этот", SpeechPart.APRO),
                    new("чудесный", SpeechPart.A)
                }) {TestName = "Normalized words"};

            yield return new TestCaseData(
                    new List<string> {"приветик", "чудеснейший"},
                    new List<SpeechPartWord> {new("приветик", SpeechPart.S), new("чудеснейший", SpeechPart.A)})
                {TestName = "Words in different forms"};
        }
    }
}