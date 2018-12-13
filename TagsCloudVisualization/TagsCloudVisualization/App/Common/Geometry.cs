using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Geometry
    {
        public static bool IsRectangleIntersection(Rectangle rectangle, Rectangle other)
        {
            return !(rectangle.X + rectangle.Width < other.X || other.X + other.Width < rectangle.X ||
                     rectangle.Y + rectangle.Height < other.Y || other.Y + other.Height < rectangle.Y);
        }

        public static double GetMaxDistanceToRectangle(Point point, Rectangle rectangle)
        {
            return GetVertices(rectangle).Max(v => GetDistanceBetweenPoints(point, v));
        }

        public static IEnumerable<Point> GetVertices(Rectangle rectangle)
        {
            yield return new Point(rectangle.Right, rectangle.Top);
            yield return new Point(rectangle.Right, rectangle.Bottom);
            yield return new Point(rectangle.Left, rectangle.Top);
            yield return new Point(rectangle.Left, rectangle.Bottom);
        }

        public static double GetDistanceBetweenPoints(Point point, Point other)
        {
            return Math.Sqrt((point.X - other.X) * (point.X - other.X) +
                             (point.Y - other.Y) * (point.Y - other.Y));
        }

        public static double GetMaxDistanceToRectangles(Point point, IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Select(rectangle => GetMaxDistanceToRectangle(point, rectangle)).Max();
        }
    }
}
