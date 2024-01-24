using System.Drawing;

namespace TagsCloud;

public static class CloudLayouterExtension
{
    public static List<Point> GetRectanglesLocation(this ICloudLayouter layouter)
    {
        return layouter.Rectangles.Select(rectangle => rectangle.Location).ToList();
    }
}