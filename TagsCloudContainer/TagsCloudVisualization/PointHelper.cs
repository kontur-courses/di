using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class PointHelper
    {
        public static Point GetTopLeftCorner(IEnumerable<Point> points) => points.Aggregate(
            (min, current) =>
                new Point(Math.Min(current.X, min.X), Math.Min(current.Y, min.Y)));

        public static Point GetBottomRightCorner(IEnumerable<Point> points) => points.Aggregate(
            (max, current) =>
                new Point(Math.Max(current.X, max.X), Math.Max(current.Y, max.Y)));
    }
}