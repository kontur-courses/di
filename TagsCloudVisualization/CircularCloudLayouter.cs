using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Configurations;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly DistributionConfiguration distributionConfiguration;

        public CircularCloudLayouter(DistributionConfiguration distributionConfiguration)
        {
            this.distributionConfiguration = distributionConfiguration;
        }

        public Rectangle GetNextRectangle(Point center, List<Rectangle> rectangles, Size nextRectangleSize)
        {
            var shiftX = -nextRectangleSize.Width / 2;
            var shiftY = -nextRectangleSize.Height / 2;

            if (rectangles.Count == 0)
                return new Rectangle(new Point(center.X + shiftX, center.Y + shiftY), nextRectangleSize);

            return new Rectangle(GetNextRectangleEnumeratePositions(center, distributionConfiguration)
                .First(position =>
                {
                    return rectangles.All(rectangle =>
                        !rectangle.IntersectsWith(new Rectangle(position, nextRectangleSize)));
                }), nextRectangleSize);
        }

        public List<Rectangle> GenerateCloud(Point center, List<Size> rectangleSizes)
        {
            var rectangles = new List<Rectangle>();

            foreach (var size in rectangleSizes)
                rectangles.Add(GetNextRectangle(center, rectangles, size));

            return rectangles;
        }

        private static IEnumerable<Point> GetNextRectangleEnumeratePositions(Point startPoint, DistributionConfiguration distributionConfiguration)
        {
            var angle = 0.0f;
            
            while (true)
            {
                angle += distributionConfiguration.ShiftAngle;
                var x = startPoint.X + distributionConfiguration.ShiftX * angle * Math.Cos(angle);
                var y = startPoint.Y + distributionConfiguration.ShiftY * angle * Math.Sin(angle);

                yield return new Point((int)x, (int)y);
            }
        }
    }
}