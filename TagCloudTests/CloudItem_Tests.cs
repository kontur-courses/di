using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using WordCloudImageGenerator;
using WordCloudImageGenerator.Parsing.Word;

namespace TagCloudTests
{
    public class CloudItem_Tests
    {
        [Test]
        public void Ctor_ItemContainsGivenRectangle()
        {
            var rect = new Rectangle(new Point(0, 0), new Size(100, 100));
            var word = new Word("word", 5);
            var font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold);
            var item = new CloudItem(rect, word, font);

            item.Font.Should().BeEquivalentTo(font);
        }

        [Test]
        public void Ctor_ItemContainsGivenWord()
        {
            var rect = new Rectangle(new Point(0, 0), new Size(100, 100));
            var word = new Word("word", 5);
            var font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold);
            var item = new CloudItem(rect, word, font);

            item.Word.Should().BeEquivalentTo(word);
        }

        [Test]
        public void Ctor_ItemContainsGivenFont()
        {
            var rect = new Rectangle(new Point(0, 0), new Size(100, 100));
            var word = new Word("word", 5);
            var font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold);
            var item = new CloudItem(rect, word, font);

            item.Rectangle.Should().BeEquivalentTo(rect);
        }
    }
}
