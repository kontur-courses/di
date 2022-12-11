using System.Drawing;

namespace TagCloudTest.CloudLayouter.SpiralCloudLayouters.Infrastructure;

public static class TagCloudDrawer
{
    public static Bitmap DrawWithAutoSize(
        Rectangle[] rectangles,
        Color bgColor, Color rectangleColor,
        bool drawCenter = false, bool drawCircle = false)
    {
        var center = FindCenter(rectangles);
        var size = new Size(center.X * 2, center.Y * 2);
        var cloudDrawingSettings =
            new CloudDrawingSettings(size, center, bgColor, rectangleColor, drawCenter, drawCircle);
        return Draw(rectangles, cloudDrawingSettings);
    }
        
    private static Bitmap Draw(
        Rectangle[] rectangles,
        CloudDrawingSettings settings)
    {
        using var myBitmap = new Bitmap(settings.Size.Width, settings.Size.Height);
        var graphics = Graphics.FromImage(myBitmap);
        graphics.Clear(settings.BgColor);

        foreach (var rectangle in rectangles)
        {
            graphics.DrawRectangle(new Pen(settings.RectangleColor, 2), rectangle);
        }

        if (settings.DrawCenter) DrawCenter(graphics, settings.Center);
        if (settings.DrawCircle) DrawMaxCircle(graphics, settings.Center, rectangles);

        return myBitmap;
    }

    private static Point FindCenter(IReadOnlyCollection<Rectangle> rectangles)
    {
        if (rectangles.Count == 0)
            throw new ArgumentException("rectangles can not be empty");
        var firstRectangle = rectangles.First();
        var centerX = firstRectangle.Left + firstRectangle.Width / 2;
        var centerY = firstRectangle.Top + firstRectangle.Height / 2;
        return new Point(centerX, centerY);
    }
        
    private static void DrawCenter(Graphics g, Point center)
    {
        g.DrawEllipse(new Pen(Color.Blue, 2), center.X - 4, center.Y - 4, 8, 8);
    }

    private static void DrawMaxCircle(Graphics g, Point center, IEnumerable<Rectangle> rectangles)
    {
        var maxDistance = rectangles
            .Select(rect => GetMaxDistanceToNode(rect, center))
            .Max();
        g.DrawEllipse(
            new Pen(Color.Blue, 2),
            center.X - (int)maxDistance,
            center.Y - (int)maxDistance,
            (int)maxDistance * 2,
            (int)maxDistance * 2);
    }

    private static double GetDistance(Point a, Point b)
    {
        return Math.Sqrt(Math.Pow(a.X - b.X, 2) +
                         Math.Pow(a.Y - b.Y, 2));
    }

    private static double GetMaxDistanceToNode(Rectangle rectangle, Point center)
    {
        var nodes = new[]
        {
            rectangle.Location,
            new Point(rectangle.Right, rectangle.Top),
            new Point(rectangle.Left, rectangle.Bottom),
            new Point(rectangle.Right, rectangle.Bottom)
        };
        return nodes
            .Select(node => GetDistance(node, center))
            .Max();
    }
}