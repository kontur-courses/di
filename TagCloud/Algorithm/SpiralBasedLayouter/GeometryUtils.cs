using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Algorithm.SpiralBasedLayouter
{
    public class GeometryUtils
    {
        public static Point ConvertPolarToIntegerCartesian(double r, double phi)
        {
            var x = (int)Math.Round(r * Math.Cos(phi));
            var y = (int)Math.Round(r * Math.Sin(phi));
            return new Point(x, y);
        }

        public static IEnumerable<Point> BuildConvexHull(IEnumerable<Rectangle> rectangles)
        {
            var points = GetAllCornersPoints(rectangles).OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            var upperHull = GetHullHalf(points, true);
            var lowerHull = GetHullHalf(points, false);
            return upperHull.Concat(lowerHull).Distinct();
        }

        private static List<Point> GetHullHalf(List<Point> points, bool upper)
        {
            var start = upper ? 0 : points.Count - 1;
            bool IsStop(int i) => upper ? i < points.Count : i >= 0;
            var delta = upper ? 1 : -1;

            var hull = new List<Point>();
            for (var i = start; IsStop(i); i += delta)
            {
                var point = points[i];
                ProcessPoint(hull, point);
                hull.Add(point);
            }
            hull.RemoveAt(hull.Count - 1);
            return hull;
        }

        private static void ProcessPoint(List<Point> hull, Point point)
        {
            while (hull.Count >= 2)
            {
                var lastIndex = hull.Count - 1;
                var vector = new Segment(hull[lastIndex - 1], hull[lastIndex]);
                if (!IsRightTurn(vector, point))
                    hull.RemoveAt(lastIndex);
                else
                    break;
            }
        }

        private static bool IsRightTurn(Segment vector, Point point)
        {
            return (vector.Right.X - vector.Left.X) * (point.Y - vector.Left.Y)
                   < (vector.Right.Y - vector.Left.Y) * (point.X - vector.Left.X);
        }

        public static List<Point> GetAllCornersPoints(IEnumerable<Rectangle> rectangles)
        {
            return rectangles.SelectMany(RectangleUtils.GetRectangleCorners).ToList();
        }
    }
}
