using System.Drawing;

namespace TagsCloud;

public static class  CloudLayouterExtension
{
    public static List<Point> GetRectanglesLocation(this ICircularCloudLayouter layouter)
    {
        return layouter.Rectangles.Select(rectangle => rectangle.Location).ToList();
    }
}