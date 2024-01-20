using System.Drawing;

namespace TagCloud;

public static class CloudDrawer
{
    public static Bitmap DrawTagCloud(IList<Rectangle> rectangles, int border = 10)
    {
        var imageSize = GetImageSize(rectangles, border);
        var shift = GetImageShift(rectangles, border);
        var image = new Bitmap(imageSize.Width, imageSize.Height);
        using var graphics = Graphics.FromImage(image);
        foreach (var rectangle in rectangles)
        {
            var shiftedCoordinates = new Point(rectangle.X - shift.Width, rectangle.Y - shift.Height);
            using var brush = new SolidBrush(GetRandomColor());
            graphics.FillRectangle(brush, new Rectangle(shiftedCoordinates, rectangle.Size));
        }

        return image;
    }

    private static Size GetImageShift(IList<Rectangle> rectangles, int border)
    {
        var minX = rectangles.Min(rectangle => rectangle.Left);
        var minY = rectangles.Min(rectangle => rectangle.Top);

        return new Size(minX - border, minY - border);
    }

    private static Size GetImageSize(IList<Rectangle> rectangles, int border)
    {
        var minX = rectangles.Min(rectangle => rectangle.Left);
        var maxX = rectangles.Max(rectangle => rectangle.Right);
        var minY = rectangles.Min(rectangle => rectangle.Top);
        var maxY = rectangles.Max(rectangle => rectangle.Bottom);

        return new Size(maxX - minX + 2 * border, maxY - minY + 2 * border);
    }

    private static Color GetRandomColor()
    {
        var random = new Random();
        return Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
    }
}