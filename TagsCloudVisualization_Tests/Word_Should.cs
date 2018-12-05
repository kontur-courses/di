using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class Word_Should
    {
        [Test]
        public void HaveValidProperties_AfterCreation()
        {
            var text = "letter";
            var font = new Font(FontFamily.GenericSansSerif, 14);
            var rect = new Rectangle(new Point(0, 0), new Size(100, 100));
            var word = new Word(text, font, rect);
            word.Text.Should().Be(text);
            word.Font.Should().Be(font);
            word.Rectangle.Should().Be(rect);
        }
    }
}