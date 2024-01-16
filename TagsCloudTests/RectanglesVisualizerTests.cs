using System.Drawing;
using System.Drawing.Imaging;
using FluentAssertions;
using TagsCloud;

namespace TagsCloudTests;

public class RectanglesVisualizerTests
{
    private const int MinCoordinate = 0;
    private const int MaxCoordinate = 5000;
    private static readonly Random Random = new();

    [Test]
    public void GetTagsCloudImage_DrawImageWithoutRectangles_EmptyImage()
    {
        var rectangles = new List<Rectangle>();
        var expected = BitmapToByteArray(new Bitmap(100, 100));
        var result = BitmapToByteArray(RectanglesVisualizer.GetTagsCloudImage(rectangles));
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetTagsCloudImage_DrawSomeRectangles_AllRectanglesInImage()
    {
        var count = 10;
        var rectangles = new List<Rectangle>();
        for (var i = 0; i < count; i++)
        {
            var locate = GetRandomLocation();
            var size = Utils.GetRandomSize();
            var rect = new Rectangle(locate, size);
            rectangles.Add(rect);
        }

        var image = RectanglesVisualizer.GetTagsCloudImage(rectangles);
        CheckImageBorders(rectangles, image).Should().BeTrue();
    }

    private static Point GetRandomLocation() =>
        new Point(Random.Next(MinCoordinate, MaxCoordinate), Random.Next(MinCoordinate, MaxCoordinate));

    private static byte[] BitmapToByteArray(Bitmap bitmap)
    {
        using var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return stream.ToArray();
    }

    private static bool CheckImageBorders(List<Rectangle> rectangles, Bitmap image) =>
        rectangles.Max(rectangle => rectangle.Bottom) < image.Height &&
               rectangles.Max(rectangle => rectangle.Right) < image.Width;
}