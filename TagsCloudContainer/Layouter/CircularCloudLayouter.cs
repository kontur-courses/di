using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;

namespace TagsCloudContainer.Layouter
{
    public class CircularCloudLayouter : ILayouter
    {
        public Point Center { get; }
        private ILayouterSettings settings;

        public CircularCloudLayouter(ILayouterSettings settings)
        {
            this.settings = settings;
            Center = settings.Center;
        }

        private void AddCorners(Rectangle rectangle, SortedSet<Point> corners)
        {
            foreach (var corner in rectangle.GetCorners())
                corners.Add(corner);
        }

        private List<Rectangle> GetRectanglesNearThePoint(Point point, Size size)
        {
            return new List<Rectangle>
            {
                new Rectangle(new Point(point.X, point.Y - size.Height), size),
                new Rectangle(point - size, size),
                new Rectangle(new Point(point.X - size.Width, point.Y), size),
                new Rectangle(point, size)
            };
        }

        private bool IsRectangleIntersectsWithOtherRectangles(Rectangle rectangle, List<Rectangle> rectangles)
        {
            return rectangles.Any(rectangle.IntersectsWith);
        }

        private int GetSuitabilityCoefficient(Rectangle rectangle)
        {
            var corners = rectangle.GetCorners();
            return corners.Sum(point => point.SquaredDistanceTo(Center));
        }

        private Rectangle GetMostSuitableRectangle(List<Rectangle> rectangles)
        {
            var minCoefficient = GetSuitabilityCoefficient(rectangles[0]);
            var mostSuitableRectangle = rectangles[0];
            foreach (var rectangle in rectangles.Skip(1))
            {
                var coefficient = GetSuitabilityCoefficient(rectangle);
                if (coefficient < minCoefficient)
                {
                    minCoefficient = coefficient;
                    mostSuitableRectangle = rectangle;
                }
            }
            return mostSuitableRectangle;
        }

        private Rectangle GetRectangle(Size size, List<Rectangle> rectangles, SortedSet<Point> corners)
        {
            foreach (var corner in corners)
            {
                var nearRectangles = GetRectanglesNearThePoint(corner, size);
                var matchingRectangles = nearRectangles
                    .Where(rect => !IsRectangleIntersectsWithOtherRectangles(rect, rectangles))
                    .ToList();
                if (matchingRectangles.Count == 0)
                    continue;
                return GetMostSuitableRectangle(matchingRectangles);
            }
            throw new Exception("There was no suitable place for the rectangle");
        }

        private Rectangle PutNextRectangle(Size rectangleSize, List<Rectangle> rectangles, SortedSet<Point> corners)
        {
            Rectangle rectangle;
            if (rectangles.Count == 0)
            {
                var location = Center - new Size(rectangleSize.Width / 2, rectangleSize.Height / 2);
                rectangle = new Rectangle(location, rectangleSize);
            }
            else
                rectangle = GetRectangle(rectangleSize, rectangles, corners);

            rectangles.Add(rectangle);
            AddCorners(rectangle, corners);
            return rectangle;
        }

        public IList<Rectangle> GetRectangles(IEnumerable<Size> sizes)
        {
            var rectangles = new List<Rectangle>();
            var corners = new SortedSet<Point>(new PointsRadiusComparer(Center));
            foreach (var size in sizes)
                PutNextRectangle(size, rectangles, corners);
            return rectangles;
        }
    }
}
