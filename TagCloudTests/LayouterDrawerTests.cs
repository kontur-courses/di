using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;

namespace TagCloudTests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class LayouterDrawerTests
{
    [Test]
    public void ImageSize_ThrowArgumentException_OnNegativeSize()
    {
        var drawer = new TagCloudDrawer();
        var size = new Size(-1, -1);

        var act = () => drawer.ImageSize = size;

        act.Should().Throw<ArgumentException>()
            .WithMessage($"Width and height of the image must be positive, but {size}");
    }

    [Test]
    public void Draw_Bitmap_FilledWithBackgroundColor()
    {
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var backgroundColor = Color.FromArgb(255, 255, 255);
        var drawer = new TagCloudDrawer { BackgroundColor = backgroundColor };

        var bitmap = drawer.Draw(layouter);

        AllPixels(bitmap).Should().OnlyContain(c => c == backgroundColor);
    }

    [Test]
    public void Draw_DrawSomeRectangles()
    {
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        layouter.PutNextRectangle(new Size(10, 10));
        var backgroundColor = Color.FromArgb(255, 255, 255);
        var penColor = Color.FromArgb(255, 255, 255);
        var drawer = new TagCloudDrawer
        {
            BackgroundColor = backgroundColor,
            RectanglesPen = new Pen(penColor, 1)
        };

        var bitmap = drawer.Draw(layouter);

        AllPixels(bitmap).Should().Contain(penColor);
    }

    [Test]
    public void Draw_DifferentCloudsBitmaps_AreDifferent()
    {
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        layouter.PutNextRectangle(new Size(10, 10));
        var drawer = new TagCloudDrawer();

        var bitmap1 = drawer.Draw(layouter);
        layouter.PutNextRectangle(new Size(10, 10));
        var bitmap2 = drawer.Draw(layouter);

        AllPixels(bitmap1).Should().NotEqual(AllPixels(bitmap2));
    }

    private static IEnumerable<Color> AllPixels(Bitmap bitmap)
    {
        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
            yield return bitmap.GetPixel(x, y);
    }
}