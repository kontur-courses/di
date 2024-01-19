using System.Diagnostics.Metrics;
using System.Drawing;

namespace TagCloudTests;

public static class Extensions
{
    public static IEnumerable<Tuple<T, T>> CartesianProduct<T>(this IEnumerable<T> source)
    {
        var array = source.ToArray();
        if (array.Length <= 1)
            throw new ArgumentException();
        for (int i = 0; i < array.Length; i++)
            for (int j = i + 1; j < array.Length; j++)
                yield return Tuple.Create(array[i], array[j]);
    }

    public static bool HasIntersectedRectangles(this IEnumerable<Rectangle> rectangles)
    {
        return rectangles
            .SelectMany(
                (x, i) => rectangles.Skip(i + 1), 
                (x, y) => Tuple.Create(x, y)
            )
            .Any(tuple => tuple.Item1.IntersectsWith(tuple.Item2));
    }

    public static double GetDistanceTo(this Point first, Point second)
    {
        return Math.Sqrt((first.X - second.X) * (first.X - second.X) + (first.Y - second.Y) * (first.Y - second.Y));
    }
}