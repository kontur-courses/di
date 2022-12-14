using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.Abstractions;

namespace TagCloudTests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class BaseCloudDrawerTests
{
    [TestCase(-1, TestName = "{m}IsNegative")]
    [TestCase(0, TestName = "{m}IsZero")]
    [TestCase(11, TestName = "{m}GreaterThanMaxFontSize")]
    public void SetFontSize_ShouldThrowArgumentException_OnMinFontSize(int minFontSize)
    {
        var drawer = new BaseCloudDrawer(new Size(10, 10))
        {
            MaxFontSize = 10,
            MinFontSize = 5
        };

        var act = () => drawer.MinFontSize = minFontSize;

        act.Should().Throw<ArgumentException>()
            .WithMessage($"MinFonSize should be greater than 0 and less than MaxFontSize, but {minFontSize}");
    }

    [TestCase(-1, TestName = "{m}IsNegative")]
    [TestCase(0, TestName = "{m}IsZero")]
    [TestCase(4, TestName = "{m}LessThanMinFontSize")]
    public void SetFontSize_ShouldThrowArgumentException_OnMaxFontSize(int maxFontSize)
    {
        var drawer = new BaseCloudDrawer(new Size(10, 10))
        {
            MaxFontSize = 10,
            MinFontSize = 5
        };

        var act = () => drawer.MaxFontSize = maxFontSize;

        act.Should().Throw<ArgumentException>()
            .WithMessage($"MaxFontSize should be greater than 0 and MinFontSize, but {maxFontSize}");
    }

    [Test]
    public void Constructor_ShouldThrowArgumentException_OnNegativeSize()
    {
        var size = new Size(-1, -1);

        var act = () => new BaseCloudDrawer(size);

        act.Should().Throw<ArgumentException>()
            .WithMessage($"Width and height of the image must be positive, but {size}");
    }

    [Test]
    public void Draw_ResultBitmap_ShouldFilledWithBackgroundColor_OnEmptyTagsCollection()
    {
        var backgroundColor = Color.FromArgb(255, 255, 255);
        var drawer = new BaseCloudDrawer(new Size(100, 100)) { BackgroundColor = backgroundColor };

        var bitmap = drawer.Draw(Array.Empty<IDrawableTag>());

        AllPixels(bitmap).Should().OnlyContain(c => c == backgroundColor);
    }

    [TestCase(0, TestName = "{m}ZeroFontSize")]
    [TestCase(-1, TestName = "{m}NegativeFontSize")]
    public void Draw_ThrowArgumentException_OnTagWith(int fontsize)
    {
        var drawer = new BaseCloudDrawer(new Size(100, 100));
        var tags = new[] { new DrawableTag(new Tag("word", 0), fontsize, new Point(0, 0)) };

        var act = () => drawer.Draw(tags);

        act.Should().Throw<ArgumentException>()
            .WithMessage($"Weight of Tag should be greater than 0, but {fontsize}");
    }


    [Test]
    public void Draw_ShouldDrawSomeRectangles()
    {
        var backgroundColor = Color.FromArgb(255, 255, 255);
        var textColor = Color.FromArgb(255, 255, 255);
        var drawer = new BaseCloudDrawer(new Size(100, 100))
        {
            BackgroundColor = backgroundColor,
            TextColor = textColor
        };
        var tags = new[] { new DrawableTag(new Tag("word", 0), 10, new Point(0, 0)) };

        var bitmap = drawer.Draw(tags);

        AllPixels(bitmap).Should().Contain(textColor);
    }

    [Test]
    public void DrawTagCloud_DifferentCloudsBitmaps_AreDifferent()
    {
        var backgroundColor = Color.FromArgb(255, 255, 255);
        var textColor = Color.FromArgb(0, 0, 0);
        var drawer = new BaseCloudDrawer(new Size(100, 100))
        {
            BackgroundColor = backgroundColor,
            TextColor = textColor
        };
        var tags = Array.Empty<DrawableTag>();

        var bitmap1 = drawer.Draw(tags);
        tags = new[] { new DrawableTag(new Tag("word", 0), 10, new Point(0, 0)) };
        var bitmap2 = drawer.Draw(tags);

        AllPixels(bitmap1).Should().NotEqual(AllPixels(bitmap2));
    }

    private static IEnumerable<Color> AllPixels(Bitmap bitmap)
    {
        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
            yield return bitmap.GetPixel(x, y);
    }
}