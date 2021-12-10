using System.Linq;
using DeepMorphy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class RussianWordsPreparator_Should
    {
        private readonly RussianWordsConverter sut = new(new MorphAnalyzer());

        [Test]
        public void Trim()
        {
            var input = new[]
            {
                " пробел",
                "пробел "
            };
            var expected = new[]
            {
                "пробел",
                "пробел"
            };

            AssertLemma(input, expected);
        }

        [Test]
        public void LowerWords()
        {
            var input = new[]
            {
                "Начало",
                "сеРедина",
                "конеЦ"
            };

            var expected = new[]
            {
                "начало",
                "середина",
                "конец"
            };
            AssertLemma(input, expected);
        }

        [TestCase("молоток", ExpectedResult = SpeechPart.Noun)]
        [TestCase("хороший", ExpectedResult = SpeechPart.Adjective)]
        [TestCase("понимаю", ExpectedResult = SpeechPart.Verb)]
        [TestCase("понимать", ExpectedResult = SpeechPart.Verb)]
        [TestCase("Он", ExpectedResult = SpeechPart.Pronoun)]
        [TestCase("два", ExpectedResult = SpeechPart.Num)]
        [TestCase("фыв", ExpectedResult = SpeechPart.Unknown)]
        public SpeechPart IdentifySpeechParts(string word)
        {
            var input = new[] {word};
            return sut.Convert(input).First().SpeechPart;
        }

        [Test]
        public void ParseLines_WhenSeveralWords()
        {
            var input = new[] {"он один"};
            var result = sut.Convert(input);
            result.Should().HaveCount(2);
        }

        private void AssertLemma(string[] input, string[] expected)
        {
            sut.Convert(input)
                .Select(wi => wi.Lemma)
                .Should().BeEquivalentTo(expected);
        }
    }
}