using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App;
using TagsCloud.Infrastructure;

namespace TagsCloudTests
{
    [TestFixture]
    internal class TagsCloudDrawerTests
    {
        private readonly TagsCloudSettings defaultSettings = TagsCloudSettings.DefaultSettings;

        [Test]
        public void GetTagsFromWords_ShouldThrow_IfCollectionIsNull()
        {
            var drawer = new TagsCloudDrawer(new RectanglesLayouter(Point.Empty), TagsCloudSettings.DefaultSettings);
            var graphics =
                Graphics.FromImage(new Bitmap(defaultSettings.ImageSize.Width, defaultSettings.ImageSize.Height));
            Action action = () => drawer.GetTagsFromWords(null, graphics).ToArray();
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetTagsFromWords_ShouldSetBiggerWord_ThatHasgreaterWeight()
        {
            var drawer = new TagsCloudDrawer(new RectanglesLayouter(Point.Empty), TagsCloudSettings.DefaultSettings);
            var graphics =
                Graphics.FromImage(new Bitmap(defaultSettings.ImageSize.Width, defaultSettings.ImageSize.Height));
            var tags = drawer.GetTagsFromWords(new[] {new Word("abc", 4), new Word("abc", 5)}, graphics).ToArray();
            tags.Length.Should().Be(2);
            var firstTag = tags[0];
            var secondTag = tags[1];
            secondTag.Rectangle.Width.Should().BeGreaterThan(firstTag.Rectangle.Width);
            secondTag.Rectangle.Height.Should().BeGreaterThan(firstTag.Rectangle.Height);
        }

        [Test]
        public void GetTagsFromWords_ShouldSetBiggerWord_ThatHasGreaterLength()
        {
            var drawer = new TagsCloudDrawer(new RectanglesLayouter(Point.Empty), TagsCloudSettings.DefaultSettings);
            var graphics =
                Graphics.FromImage(new Bitmap(defaultSettings.ImageSize.Width, defaultSettings.ImageSize.Height));
            var tags = drawer.GetTagsFromWords(new[] {new Word("abcdef", 4), new Word("abc", 5)}, graphics).ToArray();
            tags.Length.Should().Be(2);
            var firstTag = tags[0];
            var secondTag = tags[1];
            secondTag.Rectangle.Width.Should().BeLessThan(firstTag.Rectangle.Width);
        }

        [Test]
        public void GetTagsFromWords_ShouldFormSize_DependOnTheFont()
        {
            var settings = TagsCloudSettings.DefaultSettings;
            var drawer = new TagsCloudDrawer(new RectanglesLayouter(Point.Empty), settings);
            var graphics =
                Graphics.FromImage(new Bitmap(defaultSettings.ImageSize.Width, defaultSettings.ImageSize.Height));
            settings.CurrentFontStyle = FontStyle.Regular;
            var firstTag = drawer.GetTagsFromWords(new[] {new Word("abc", 4)}, graphics).First();
            settings.CurrentFontStyle = FontStyle.Italic;
            var secondtag = drawer.GetTagsFromWords(new[] {new Word("abc", 4)}, graphics).First();
            firstTag.Rectangle.Size.Should().NotBeEquivalentTo(secondtag.Rectangle);
        }

        [Test]
        public void DrawTagsCloud_ShouldThrow_IfCollectionIsNull()
        {
            var drawer = new TagsCloudDrawer(new RectanglesLayouter(Point.Empty), TagsCloudSettings.DefaultSettings);
            var graphics =
                Graphics.FromImage(new Bitmap(defaultSettings.ImageSize.Width, defaultSettings.ImageSize.Height));
            Action action = () => drawer.DrawTagsCloud(null, PointF.Empty, graphics);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetTagsCloud_ShouldThrow_IfCollectionIsNull()
        {
            var drawer = new TagsCloudDrawer(new RectanglesLayouter(Point.Empty), TagsCloudSettings.DefaultSettings);
            Action action = () => drawer.GetTagsCloud(null);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetTagsCloud_ShouldFormImage_WithSetSize()
        {
            var drawer = new TagsCloudDrawer(new RectanglesLayouter(Point.Empty), TagsCloudSettings.DefaultSettings);
            var tagsCloud = drawer.GetTagsCloud(new Word[0]);
            tagsCloud.Height.Should().Be(defaultSettings.ImageSize.Height);
            tagsCloud.Width.Should().Be(defaultSettings.ImageSize.Width);
        }
    }
}