using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.PointGenerators;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly IPointGenerator pointGenerator;
        private readonly IPointGeneratorSettings pointGeneratorSettings;
        private readonly List<Rectangle> rectangles;

        public CircularCloudLayouter(IPointGenerator pointGenerator, IPointGeneratorSettings pointGeneratorSettings)
        {
            this.pointGenerator = pointGenerator;
            this.pointGeneratorSettings = pointGeneratorSettings;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size size)
        {
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException("Lengths of size must be positive");

            var nextRect = GetNextNotIntersectingRectangle(size);
            rectangles.Add(nextRect);

            return nextRect;
        }

        public IReadOnlyList<Rectangle> GetRectangles()
        {
            return rectangles;
        }

        private Rectangle GetNextNotIntersectingRectangle(Size size)
        {
            Rectangle nextRectangle;
            do
            {
                nextRectangle = GetNextRectangle(size);
            } while (nextRectangle.IntersectsWith(rectangles));

            return nextRectangle;
        }

        private Rectangle GetNextRectangle(Size size)
        {
            var nextPoint = pointGenerator.GetNextPoint(pointGeneratorSettings);
            return new Rectangle(nextPoint.X, nextPoint.Y, size.Width, size.Height);
        }
    }
}