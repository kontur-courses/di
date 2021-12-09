using System.Collections.Generic;
using System.Linq;
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

        [Timeout(1000)]
        [TestCase("1", TestName = "Number")]
        [TestCase("h", TestName = "Eng letter")]
        [TestCase("!", TestName = "Sign")]
        [TestCase(" ", TestName = "Space")]
        public void ParseWord_SkipWords_ThatContain(string forbiddenPart)
        {
            var words = new[]
                {"привет", forbiddenPart, $"привет{forbiddenPart}", $"{forbiddenPart}привет", $"при{forbiddenPart}вет"};

            parser.ParseWords(words)
                .Should().BeEquivalentTo(new List<SpeechPartWord> {new("привет", SpeechPart.S)});
        }

        [Test]
        public void ParseWords_SupportWords_WithHyphen()
        {
            var speechPartWord = new SpeechPartWord("красно-синий", SpeechPart.A);
            parser.ParseWords(new[] {speechPartWord.Word})
                .Should().BeEquivalentTo(new List<SpeechPartWord> {speechPartWord});
        }

        [TestCaseSource(nameof(ParseWordsReturnWordInfosCases))]
        public void ParseWords_WithNormalizedWords_ReturnWordInfos(List<SpeechPartWord> speechPartWords)
        {
            var words = speechPartWords.Select(speechPartWord => speechPartWord.Word);
            parser.ParseWords(words)
                .Should().BeEquivalentTo(speechPartWords);
        }

        private static IEnumerable<TestCaseData> ParseWordsReturnWordInfosCases()
        {
            yield return new TestCaseData(
                new List<SpeechPartWord>
                {
                    new("привет", SpeechPart.S),
                    new("в", SpeechPart.PR),
                    new("этот", SpeechPart.APRO),
                    new("чудесный", SpeechPart.A)
                }) {TestName = "Normalized words"};

            yield return new TestCaseData(
                    new List<SpeechPartWord>
                    {
                        new("приветик", SpeechPart.S),
                        new("чудеснейший", SpeechPart.A)
                    })
                {TestName = "Words in different forms"};
        }
    }
}