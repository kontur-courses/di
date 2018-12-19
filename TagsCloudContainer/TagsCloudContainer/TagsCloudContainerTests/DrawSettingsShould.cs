using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.TagsCloudContainerTests
{
    [TestFixture]
    internal class DrawSettingsShould
    {
        private DrawSettings<Word> _drawSettings;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _drawSettings = new DrawSettings<Word>();
            _drawSettings.SetFilePath("filePath");
            _drawSettings.SetFontName("fontName");
            _drawSettings.SetItemPainter( r => Color.Red);
        }

        [Test]
        public void ConcatenateFilePathAndImageFileFormat()
        {
            _drawSettings.GetFullFileName().Should().Be("filePath.jpg");
        }

        [Test]
        public void SetFontName()
        {
            _drawSettings.GetFontName().Should().Be("fontName");
        }

        [Test]
        public void ChangeImageFileFormat()
        {
            _drawSettings.SetImageFileFormat(ImageFileFormat.Jpg);

            _drawSettings.GetImageFileFormat().ToString().ToLower().Should().Be("jpg");
        }

        [Test]
        public void ItemPainterShouldReturnColor()
        {
            var brush = _drawSettings.GetBrush(new ItemToDraw<Word>(new Word("word", 1), 1, 1, 1, 1));

            brush.Color.Name.Should().Be("Red");
            brush.Color.A.Should().Be(255);
            brush.Color.R.Should().Be(255);
            brush.Color.G.Should().Be(0);
            brush.Color.B.Should().Be(0);
        }

        [TestCase(0, 1, TestName = "FallOn_NotPositiveWidth")]
        [TestCase(1, 0, TestName = "FallOn_NotPositiveHeight")]
        [TestCase(-1, 1, TestName = "FallOn_NegativeWidth")]
        [TestCase(1, -1, TestName = "FallOn_NegativeHeight")]
        public void SetImageSizeShould(int width, int height)
        {
            Action act = () => _drawSettings.SetImageSize(new Size(width, height));

            act.ShouldThrow<ArgumentException>().WithMessage("both image size parameters should be positive");
        }
    }
}
