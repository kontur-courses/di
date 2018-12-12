using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordFormatters;

namespace TagsCloudContainer.Tests
{
    public class SimpleFormatterTests
    {
        private readonly Font font = new Font(FontFamily.GenericMonospace, 12);
        private readonly Color color = Color.MidnightBlue;
        private IWordFormatter formatter;
        private IWordsWeighter wordsWeighter;

        [SetUp]
        public void DoBeforeAnyTest()
        {
            wordsWeighter = A.Fake<IWordsWeighter>();
            var config = new Config {Font = font, Color = color};
            formatter = new SimpleFormatter(wordsWeighter, config);
        }

        [Test]
        public void FormatWords_ReturnsEmptyResult_OnEmptyWords()
        {
            var data = Enumerable.Empty<string>();

            var formattedWords = formatter.FormatWords(data);

            formattedWords
                .Should()
                .BeEquivalentTo(data);

            A.CallTo(() => wordsWeighter.GetWordsWeight(null))
                .WithAnyArguments()
                .MustNotHaveHappened();
        }

        [Test]
        public void FormatWords_WorksCorrectly()
        {
            var words = new List<string>
            {
                "hello",
                "hello",
                "world",
                "from",
                "test"
            };

            var expected = new List<Word>
            {
                new Word(font, color, "hello"),
                new Word(GenerateFont(1), color, "world"),
                new Word(GenerateFont(2), color, "from"),
                new Word(GenerateFont(3), color, "test"),
            };

            ConfigureWordsWeighter(words);

            var formattedWords = formatter.FormatWords(words);

            formattedWords
                .Should()
                .BeEquivalentTo(expected);

            AssertFormatSuccessful();
        }

        [Test]
        public void FormatWords_SetsValueToTheWord()
        {
            var words = new List<string>
            {
                "hello",
                "hello",
                "world",
                "from",
                "test"
            };

            ConfigureWordsWeighter(words);

            var formattedWords = formatter.FormatWords(words);

            formattedWords
                .Select(z => z.Value)
                .Should()
                .BeEquivalentTo(words.Skip(1));

            AssertFormatSuccessful();
        }

        public void ConfigureWordsWeighter(IEnumerable<string> words)
        {
            var result = new Dictionary<string, int>();
            var count = 0;

            foreach (var word in words.Distinct())
            {
                result[word] = count++;
            }

            A.CallTo(() => wordsWeighter.GetWordsWeight(null))
                .WithAnyArguments()
                .Returns(result);
        }

        public void AssertFormatSuccessful()
        {
            A.CallTo(() => wordsWeighter.GetWordsWeight(null))
                .WithAnyArguments()
                .MustHaveHappenedOnceExactly();
        }

        public Font GenerateFont(int wordsWeight)
        {
            return new Font(font.FontFamily, font.Size + wordsWeight * 7);
        }
    }
}