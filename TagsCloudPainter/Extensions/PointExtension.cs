using System.Drawing;

namespace TagsCloudPainter.Extensions;

public static class PointExtension
{
    public static Rectangle GetRectangle(this Point center, Size size)
    {
        var x = center.X - size.Width / 2;
        var y = center.Y - size.Height / 2;

        return new Rectangle(new Point(x, y), size);
    }
}