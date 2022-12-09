using System.Drawing;

namespace TagsCloudVisualization;

public static class RectangleExtensions
{
    public static bool CheckForIntersectionWithRectangles(this Rectangle rectangle, List<Rectangle> rectangles)
    {
        foreach (var curRectangle in rectangles)
            if (rectangle.IntersectsWith(curRectangle))
                return true;

        return false;
    }
}