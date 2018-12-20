using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloudLayouter.App
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly SortedSet<Point> anchorpoints;
        private readonly Point center;
        private readonly HashSet<Rectangle> rectangles;

        public CircularCloudLayouter(ImageSettings imageSettings)
        {
            center = new Point(imageSettings.Width / 2, imageSettings.Height / 2);
            anchorpoints = new SortedSet<Point>(new PointDistanceComparer(center));
            rectangles = new HashSet<Rectangle>();

            anchorpoints.Add(center);
        }

        public Rectangle PutNextRectangle(Size size)
        {
            if (size.IsEmpty)
                throw new ArgumentException("Size of rectangle can't be Empty.");

            var newRectangle = SearchBestValidPlace(size);
            AddAnchorPoints(newRectangle);
            rectangles.Add(newRectangle);
            return newRectangle;
        }

        private Rectangle SearchBestValidPlace(Size size)
        {
            var newRectangle = new Rectangle(new Point(), size);

            foreach (var point in anchorpoints)
            foreach (var possibleLocationPoint in point.GetPossibleTagLocation(size))
            {
                newRectangle.Location = possibleLocationPoint;
                if (!newRectangle.IntersectsWith(rectangles))
                    return newRectangle;
            }

            return newRectangle;
        }

        private void AddAnchorPoints(Rectangle rectangle)
        {
            foreach (var point in rectangle.GetCornerPoints()) anchorpoints.Add(point);
        }
    }

    public static class RectangleExtensions
    {
        public static bool IntersectsWith(this Rectangle basicRectangle, IEnumerable<Rectangle> rectEnum)
        {
            return rectEnum.Any(rectangle => rectangle.IntersectsWith(basicRectangle));
        }

        public static IEnumerable<Point> GetCornerPoints(this Rectangle rectangle)
        {
            yield return new Point(rectangle.Left, rectangle.Top);
            yield return new Point(rectangle.Left, rectangle.Bottom);
            yield return new Point(rectangle.Right, rectangle.Top);
            yield return new Point(rectangle.Right, rectangle.Bottom);
        }
    }

    public static class PointExtensions
    {
        public static double GetDistance(this Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public static IEnumerable<Point> GetPossibleTagLocation(this Point point, Size size)
        {
            yield return point;
            yield return new Point(point.X - size.Width, point.Y);
            yield return new Point(point.X, point.Y - size.Height);
            yield return new Point(point.X - size.Width, point.Y - size.Height);
        }
    }

    public class PointDistanceComparer : IComparer<Point>
    {
        private readonly Point anchorPoint;

        public PointDistanceComparer(Point anchorPoint)
        {
            this.anchorPoint = anchorPoint;
        }

        public int Compare(Point x, Point y)
        {
            if (x.GetDistance(anchorPoint) > y.GetDistance(anchorPoint))
                return 1;
            return -1;
        }
    }
}