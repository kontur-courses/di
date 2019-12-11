using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
    [CliElement("circular", typeof(CircularCloudLayouter))]
    public class CircularCloudLayouter : ICloudLayouter
    {
        private float spiralCounter;

        public Rectangle PutNextRectangle(Size rectangleSize, List<Rectangle> container)
        {
            var rect = GetNextEmptyRectangleAtSpiral(rectangleSize, container);
            container.Add(rect);
            return rect;
        }

        private Rectangle GetNextEmptyRectangleAtSpiral(Size rectangleSize, List<Rectangle> container)
        {
            if (container is null)
                throw new ArgumentException("Container shouldn't be null");

            return GetPointsOnSpiral()
                .Select(p => new Rectangle(p - rectangleSize / 2, rectangleSize))
                .SkipWhile(r => container.Any(r.IntersectsWith))
                .FirstOrDefault();
        }

        private IEnumerable<Point> GetPointsOnSpiral()
        {
            while (true)
            {
                var distanceFromCenter = MathF.Sqrt(spiralCounter);
                var x = (int) (distanceFromCenter * MathF.Cos(spiralCounter));
                var y = (int) (distanceFromCenter * MathF.Sin(spiralCounter++));
                yield return new Point(x, y);
            }
        }
    }
}