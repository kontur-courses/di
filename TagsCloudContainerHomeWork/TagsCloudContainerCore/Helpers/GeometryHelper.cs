using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainerCore.Helpers;

public static class GeometryHelper
{
    public static float GetDistanceBetweenPoints(PointF p1, PointF p2)
    {
        var x = p2.X - p1.X;
        var y = p2.Y - p1.Y;

        return MathF.Sqrt(x * x + y * y);
    }

    public static Point GetRectangleCenter(Rectangle rect)
    {
        var x = rect.X + (int)MathF.Ceiling(rect.Width / 2.0f);
        var y = rect.Y + (int)MathF.Ceiling(rect.Height / 2.0f);

        return new Point(x, y);
    }

    public static Point GetRectangleLocationFromCenter(Point rectCenter, Size size)
    {
        var x = (int)MathF.Floor(rectCenter.X - size.Width / 2.0f);
        var y = (int)MathF.Floor(rectCenter.Y - size.Height / 2.0f);

        return new Point(x, y);
    }

    public static Point GetPointOnCircle(Point center, float radius, float angle)
    {
        var x = center.X + (int)MathF.Round(MathF.Cos(angle) * radius);
        var y = center.Y - (int)MathF.Round(MathF.Sin(angle) * radius);

        return new Point(x, y);
    }

    public static IEnumerable<Point> GetAllPointIntoRectangle(Rectangle rectangle)
    {
        if (rectangle.Size.IsEmpty)
            return ArraySegment<Point>.Empty;

        var xRange = Enumerable.Range(rectangle.Left + 1, rectangle.Right - rectangle.Left);
        var yRange = Enumerable.Range(rectangle.Top + 1, rectangle.Bottom - rectangle.Top);

        return xRange.SelectMany(x => yRange.Select(y => new Point(x, y)));
    }
}