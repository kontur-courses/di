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
        private readonly RussianWordsPreparator sut = new(new MorphAnalyzer(withLemmatization:true));

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
            return sut.Prepare(input).First().SpeechPart;
        }

        [Test]
        public void ParseLines_WhenSeveralWords()
        {
            var input = new[] {"он один"};
            var result = sut.Prepare(input);
            result.Should().HaveCount(2);
        }

        [TestCase("доски", ExpectedResult = "доска", TestName = "when noun has singular form")]
        [TestCase("красивые", ExpectedResult = "красивый", TestName = "when adjective")]
        [TestCase("трех", ExpectedResult = "три", TestName = "when num")]
        public string ResultWordsInInitialForm(string input)
        {
            var result = sut.Prepare(new []{input});
            return result.First().Lemma;
        }

        private void AssertLemma(string[] input, string[] expected)
        {
            sut.Prepare(input)
                .Select(wi => wi.Lemma)
                .Should().BeEquivalentTo(expected);
        }
    }
}