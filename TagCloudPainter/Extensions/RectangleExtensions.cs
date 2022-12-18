using System.Drawing;

namespace TagCloudPainter.Extensions;

public static class RectangleExtensions
{
    public static bool IsIntersectsOthersRectangles(this Rectangle rectangle, IEnumerable<Rectangle> rectangles)
    {
        if (rectangles == null)
            throw new ArgumentNullException();

        return rectangles.Any(x => x.IntersectsWith(rectangle));
    }
}