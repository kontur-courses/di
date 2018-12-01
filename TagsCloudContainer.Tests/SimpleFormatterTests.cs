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
        private Font font = new Font(FontFamily.GenericMonospace, 12);
        private Color color = Color.MidnightBlue;
        private IFormattedWord formattedWord;
        private IWordFormatter formatter;

        [SetUp]
        public void DoBeforeAnyTest()
        {
            formattedWord = new CustomFormattedWord();
            formatter = new SimpleFormatter(font, color);
        }

        [Test]
        public void FormatWords_WorksCorrectly()
        {
            var words = new List<string>()
            {
                "hello",
                "hello",
                "world",
                "from",
                "test"
            };

            var expected = new List<IFormattedWord>
            {
                new CustomFormattedWord(new Font(FontFamily.GenericMonospace, 12 + 1 * 7), color),
                new CustomFormattedWord(font, color),
                new CustomFormattedWord(font, color),
                new CustomFormattedWord(font, color),
            };

            var formattedWords = formatter.FormatWords(words);

            formattedWords
                .Should()
                .BeEquivalentTo(expected);
        }

        [Test]
        public void FormatWords_SetsValueToTheWord()
        {
            var words = new List<string>()
            {
                "hello",
                "hello",
                "world",
                "from",
                "test"
            };

            var formattedWords = formatter.FormatWords(words)
                .Select(w => (Word)w);

            formattedWords
                .Select(z => z.Value)
                .Should()
                .BeEquivalentTo(words.Skip(1));
        }

        public class CustomFormattedWord : IFormattedWord
        {
            public Font Font { get; }

            public Color Color { get; }

            public CustomFormattedWord()
            {
            }

            public CustomFormattedWord(Font font, Color color)
            {
                Font = font;
                Color = color;
            }
        }
    }
}