using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        [SetUp]
        public void DoBeforeAnyTest()
        {
            formatter = new SimpleFormatter(new WordsWeighter(), font, color);
        }

        [Test]
        public void FormatWords_ReturnsEmptyResult_OnEmptyWords()
        {
            var data = Enumerable.Empty<string>();

            var formattedWords = formatter.FormatWords(data);

            formattedWords
                .Should()
                .BeEquivalentTo(data);
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
                new Word(new Font(FontFamily.GenericMonospace, 12 + 1 * 7), color, "hello"),
                new Word(font, color, "world"),
                new Word(font, color, "from"),
                new Word(font, color, "test"),
            };

            var formattedWords = formatter.FormatWords(words);

            formattedWords
                .Should()
                .BeEquivalentTo(expected);
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

            var formattedWords = formatter.FormatWords(words);

            formattedWords
                .Select(z => z.Value)
                .Should()
                .BeEquivalentTo(words.Skip(1));
        }
    }
}