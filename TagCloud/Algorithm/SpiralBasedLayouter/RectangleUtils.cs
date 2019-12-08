using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Algorithm.SpiralBasedLayouter
{
    public class RectangleUtils
    {
        public static IEnumerable<Rectangle> GetPossibleRectangles(Point point, Size size)
        {
            var delta = new[] { 1, 0 };
            foreach (var dx in delta)
            foreach (var dy in delta)
                yield return new Rectangle(
                    new Point(point.X - dx * size.Width, point.Y - dy * size.Height),
                    size);
        }

        public static bool RectanglesAreIntersected(Rectangle firstRectangle, Rectangle secondRectangle)
        {
            return Segment.SegmentsAreIntersected(
                       new Segment(firstRectangle.Left, firstRectangle.Right),
                       new Segment(secondRectangle.Left, secondRectangle.Right))
                   && Segment.SegmentsAreIntersected(
                       new Segment(firstRectangle.Top, firstRectangle.Bottom),
                       new Segment(secondRectangle.Top, secondRectangle.Bottom));
        }

        public static Segment[] GetRectangleSides(Rectangle rectangle)
        {
            var topLeft = new Point(rectangle.Left, rectangle.Top);
            var topRight = new Point(rectangle.Right, rectangle.Top);
            var bottomLeft = new Point(rectangle.Left, rectangle.Bottom);
            var bottomRight = new Point(rectangle.Right, rectangle.Bottom);

            return new[]
            {
                new Segment(topLeft, topRight),
                new Segment(topLeft, bottomLeft),
                new Segment(topRight, bottomRight),
                new Segment(bottomLeft, bottomRight)
            };
        }

        public static Point[] GetRectangleCorners(Rectangle rectangle)
        {
            return new[]
            {
                new Point(rectangle.Left, rectangle.Top),
                new Point(rectangle.Right, rectangle.Top),
                new Point(rectangle.Left, rectangle.Bottom),
                new Point(rectangle.Right, rectangle.Bottom)
            };
        }

        public static IEnumerable<Rectangle> GetRectanglesThatCloserToPoint(Point point, Rectangle rectangle, int delta)
        {
            if (rectangle.Contains(point))
                yield break;

            var dx = Math.Sign(rectangle.X - point.X) * -delta;
            var dy = Math.Sign(rectangle.Y - point.Y) * -delta;

            var dxRectangle = new Rectangle(new Point(rectangle.X + dx, rectangle.Y), rectangle.Size);
            var dyRectangle = new Rectangle(new Point(rectangle.X, rectangle.Y + dy), rectangle.Size);
            var dxdyRectangle = new Rectangle(new Point(rectangle.X + dx, rectangle.Y + dy), rectangle.Size);

            if (ShiftedRectangleIsCloserToPoint(point, rectangle, dxRectangle))
                yield return dxRectangle;
            if (ShiftedRectangleIsCloserToPoint(point, rectangle, dyRectangle))
                yield return dyRectangle;
            if (ShiftedRectangleIsCloserToPoint(point, rectangle, dxdyRectangle))
                yield return dxdyRectangle;
        }

        public static bool ShiftedRectangleIsCloserToPoint(Point point, Rectangle originalRectangle, Rectangle shiftedRectangle)
        {
            return DistanceUtils.GetDistanceFromRectangleToPoint(point, shiftedRectangle) <
                   DistanceUtils.GetDistanceFromRectangleToPoint(point, originalRectangle);
        }

        public static double GetRectangleDiagonal(Rectangle rectangle)
        {
            return DistanceUtils.GetDistanceFromPointToPoint(
                rectangle.Location,
                new PointF(rectangle.Right, rectangle.Bottom));
        }

        public static Rectangle? GetClosestRectangleThatDoesNotIntersectWithOthers(
            Point possibleLocation, Size size, Point center, IEnumerable<Rectangle> rectangles)
        {
            var currentRectangles = GetPossibleRectangles(possibleLocation, size)
                .Where(r1 => !rectangles.Select(r2 => RectangleUtils.RectanglesAreIntersected(r1, r2)).Any(t => t)).ToList();
            Rectangle? closest = null;
            while (true)
            {
                var newClosest = DistanceUtils.GetClosestToThePointRectangle(center, currentRectangles);
                if (newClosest == null)
                    return closest;
                closest = newClosest;
                currentRectangles = GetRectanglesThatCloserToPoint(center, closest.Value, 1)
                    .Where(r1 => !rectangles.Select(r2 => RectangleUtils.RectanglesAreIntersected(r1, r2)).Any(t => t)).ToList();
            }
        }
    }
}
