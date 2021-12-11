using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace App.Implementation.GeometryUtils
{
    public static class RectangleExtension
    {
        public static bool IntersectsWithAny(this Rectangle rect, List<Rectangle> rectangles)
        {
            if (rectangles == null)
                throw new ArgumentException("rectangles list can't be null");
            return rectangles.Any(rect.IntersectsWith);
        }

        public static IEnumerable<Point> GetCorners(this Rectangle rect)
        {
            yield return new Point(rect.X, rect.Y);
            yield return new Point(rect.X + rect.Width, rect.Y);
            yield return new Point(rect.X, rect.Y + rect.Height);
            yield return new Point(rect.X + rect.Width, rect.Y + rect.Height);
        }
    }
}