using System.Drawing;

namespace TagsCloudPainter.Extensions;

public static class RectangleExtensions
{
    public static Point GetCenter(this Rectangle rectangle)
    {
        var x = rectangle.X;
        var y = rectangle.Y;
        return new Point(x + rectangle.Width / 2, y + rectangle.Height / 2);
    }
}