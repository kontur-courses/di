using System.Drawing;

namespace TagsCloudPainter.Utils;

public static class Utils
{
    public static Rectangle GetRectangleFromCenter(Point center, Size size)
    {
        var x = center.X - size.Width / 2;
        var y = center.Y - size.Height / 2;

        return new Rectangle(new Point(x, y), size);
    }

    public static Point GetRectangleCenter(Rectangle rectangle)
    {
        var x = rectangle.X;
        var y = rectangle.Y;
        return new Point(x + rectangle.Width / 2, y + rectangle.Height / 2);
    }

    public static Size GetStringSize(string value, string fontName, float fontSize)
    {
        using var graphics = Graphics.FromHwnd(IntPtr.Zero);
        using var font = new Font(fontName, fontSize);
        {
            var size = graphics.MeasureString(value, font).ToSize();
            return size;
        }
    }
}