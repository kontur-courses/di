using System.Drawing;

namespace TagCloud.Extensions;

public static class RectangleEnumerableExtensions
{
    public static bool HasIntersectedRectangles(this IEnumerable<Rectangle> rectangles)
    {
        return rectangles
            .SelectMany(
                (x, i) => rectangles.Skip(i + 1), 
                (x, y) => Tuple.Create(x, y)
            )
            .Any(tuple => tuple.Item1.IntersectsWith(tuple.Item2));
    }
    
    public static Rectangle GetMinimalContainingRectangle(this IEnumerable<Rectangle> rectangles)
    {
        int minX = int.MaxValue, minY = int.MaxValue;
        int maxX = int.MinValue, maxY = int.MinValue;

        foreach (var rectangle in rectangles)
        {
            if (rectangle.X < minX)
                minX = rectangle.X;
            if (rectangle.Y < minY)
                minY = rectangle.Y;
            if (rectangle.Right > maxX)
                maxX = rectangle.Right;
            if (rectangle.Bottom > maxY)
                maxY = rectangle.Bottom;
        }

        return new Rectangle(minX, minY, maxX - minX, maxY- minY);
    }
}