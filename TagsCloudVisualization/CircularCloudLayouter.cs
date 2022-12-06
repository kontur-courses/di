using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public Rectangle GetNextRectangle(Point center, List<Rectangle> rectangles, Size nextRectangleSize)
        {
            var shiftX = -nextRectangleSize.Width / 2;
            var shiftY = -nextRectangleSize.Height / 2;

            if (rectangles.Count == 0)
                return new Rectangle(new Point(center.X + shiftX, center.Y + shiftY), nextRectangleSize);

            return new Rectangle(GetNextRectanglePosition(center)
                .First(position =>
                {
                    return rectangles.All(rectangle =>
                        !rectangle.IntersectsWith(new Rectangle(position, nextRectangleSize)));
                }), nextRectangleSize);
        }

        public List<Rectangle> GenerateCloud(Point center, List<Size> rectangleSizes)
        {
            var rectangles = new List<Rectangle>();
            
            foreach (var rectangleSize in rectangleSizes)
                rectangles.Add(GetNextRectangle(center, rectangles, rectangleSize));

            return rectangles;
        }

        private static IEnumerable<Point> GetNextRectanglePosition(
            Point startPoint, float shiftAngle = 0.1f, float shiftX = 5.0f, float shiftY = 2.5f)
        {
            var angle = 0.0f;
            
            while (true)
            {
                angle += shiftAngle;
                var x = startPoint.X + shiftX * angle * Math.Cos(angle);
                var y = startPoint.Y + shiftY * angle * Math.Sin(angle);

                yield return new Point((int)x, (int)y);
            }
        }
    }
}