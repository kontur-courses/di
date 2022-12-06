using System.Drawing;

namespace TagsCloudVisualization;

public static class RectangleExtension
{
    public static bool IsIntersectWith(this Rectangle rectangle, IEnumerable<Rectangle> rectangles)
    {
        return rectangles.Any(x => x.IntersectsWith(rectangle));
    }
}