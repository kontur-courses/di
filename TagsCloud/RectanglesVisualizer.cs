using System.Drawing;

namespace TagsCloud;

public static class RectanglesVisualizer
{
    private const int Border = 50;

    public static Bitmap GetTagsCloudImage(List<Rectangle> rectangles)
    {
        if (!rectangles.Any()) return new Bitmap(100, 100);

        var extremePoints = GetRectanglesExtremePoints(rectangles);
        var sizeImage = GetImageSize(extremePoints);
        var shift = GetRectanglesShift(extremePoints);
        var image = new Bitmap(sizeImage.Width, sizeImage.Height);
        using var graphics = Graphics.FromImage(image);
        var background = new SolidBrush(Color.Black);
        graphics.FillRectangle(background, new Rectangle(0, 0, image.Width, image.Height));
        DrawTagsCloud(rectangles, graphics, shift);

        return image;
    }

    private static (int Left, int Right, int Top, int Bottom) GetRectanglesExtremePoints(List<Rectangle> rectangles)
    {
        var leftmost = rectangles.Min(rectangle => rectangle.Left);
        var rightmost = rectangles.Max(rectangle => rectangle.Right);
        var topmost = rectangles.Min(rectangle => rectangle.Top);
        var bottommost = rectangles.Max(rectangle => rectangle.Bottom);

        return (leftmost, rightmost, topmost, bottommost);
    }

    private static Point GetRectanglesShift((int Left, int Right, int Top, int Bottom) extremePoints)
    {
        var startX = extremePoints.Top >= 0 ? 0 : extremePoints.Top;
        var startY = extremePoints.Left >= 0 ? 0 : extremePoints.Left;

        return new Point(Math.Abs(startX) + Border, Math.Abs(startY) + Border);
    }


    private static Size GetImageSize((int Left, int Right, int Top, int Bottom) extremePoints)
    {
        var height = Math.Abs(extremePoints.Bottom) + Math.Abs(extremePoints.Top) + 2 * Border;
        var width = Math.Abs(extremePoints.Right) + Math.Abs(extremePoints.Left) + 2 * Border;

        return new Size(width, height);
    }

    private static void DrawTagsCloud(List<Rectangle> rectangles, Graphics graphics, Point shift)
    {
        foreach (var rectangle in rectangles)
        {
            var renderedRectangle =
                new Rectangle(new Point(rectangle.X + shift.X, rectangle.Y + shift.Y), rectangle.Size);
            using var pen = new Pen(Utils.GetRandomColor());
            graphics.DrawRectangle(pen, renderedRectangle);
        }
    }
}