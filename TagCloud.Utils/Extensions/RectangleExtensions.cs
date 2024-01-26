using Aspose.Drawing;

namespace TagCloud.Utils.Extensions;

public static class RectangleExtensions
{
    public static RectangleF ToRectangleF(this Rectangle rectangle)
    {
        return new RectangleF(
            rectangle.Location,
            new SizeF(rectangle.Width + 3, rectangle.Height + 3));
    }
}