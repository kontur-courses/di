using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Exceptions;
using TagsCloudVisualization.Geometry;

namespace TagsCloudVisualization.TagCloudLayouters
{
    public class CircularCloudLayouter
    {
        private readonly Point center;
        private readonly int maxCloudRadius;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();
        private readonly SortedList<double, HashSet<Point>> corners = new SortedList<double, HashSet<Point>>();

        public CircularCloudLayouter(Point center, int cloudRadius)
        {
            this.center = center;
            maxCloudRadius = cloudRadius;
            corners.Add(0, new HashSet<Point> { center });
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException($"Width and height should be greater than 0. " +
                                            $"Your size was: {rectangleSize}");

            foreach (var corner in corners.Values)
            {
                foreach (var rectangle in RectangleGeometry.GetCornerRectangles(rectangleSize, corner))
                {
                    if (rectangles.Any(rec => rec.IntersectsWith(rectangle)))
                        continue;
                    if (rectangle.AnyRectanglePointOutOfRange(center, maxCloudRadius))
                        continue;
                    rectangles.Add(rectangle);
                    AddPointsIntoList(rectangle.GetCorners());
                    return rectangle;
                }
            }   

            throw new OutOfPermissibleRangeException("We can't find the place to add your rectangle, " +
                                                     "because rectangle was out of permissible range :(. " +
                                                     $"Your size was: {rectangleSize}");
        }


        private void AddPointsIntoList(IEnumerable<Point> points)
        {
            foreach (var point in points)
            {
                var distance = point.DistanceTo(center);
                if (corners.ContainsKey(distance))
                    corners[distance].Add(point);
                else
                    corners.Add(distance, new HashSet<Point>() { point });
            }
        }
    }
}   
