using System.Drawing;

namespace TagsCloudVisualization.Extensions;

public static class RectangleExtensions
{
    public static double GetDistanceToMostDistantPoint(this IEnumerable<Rectangle> rectangles, Point fromPoint)
    {
        return rectangles.Select(x => x.GetDistanceToMostDistantPoint(fromPoint)).Max();
    }

    public static double GetDistanceToMostDistantPoint(this Rectangle rectangle, Point fromPoint)
    {
        return rectangle.GetAllPoints().Select(x => x.DistanceTo(fromPoint)).Max();
    }

    public static IEnumerable<Point> GetAllPoints(this Rectangle rectangle)
    {
        return new[]
        {
            new Point(rectangle.Left, rectangle.Top),
            new Point(rectangle.Left, rectangle.Bottom),
            new Point(rectangle.Right, rectangle.Top),
            new Point(rectangle.Right, rectangle.Bottom)
        };
    }
}