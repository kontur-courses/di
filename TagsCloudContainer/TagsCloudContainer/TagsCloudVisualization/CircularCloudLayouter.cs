using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public readonly Size Size;
        public readonly List<Rectangle> Rectangles;

        private readonly SpiralPointsGenerator pointsGenerator;

        public CircularCloudLayouter(SpiralPointsGenerator pointsGenerator)
        {
            Size = pointsGenerator.Size;
            Rectangles = new List<Rectangle>();
            this.pointsGenerator = pointsGenerator;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            {
                throw new ArgumentException($"Width and height can't be negative");
            }

            var nextRectangle = new Rectangle(GetNextPointForRectangle(rectangleSize), rectangleSize);

            if (nextRectangle.Location.X < 0 || nextRectangle.Location.Y < 0 ||
                nextRectangle.Location.X + nextRectangle.Width > Size.Width ||
                nextRectangle.Location.Y + nextRectangle.Height > Size.Height)
            {
                throw new Exception("Don't have enough space to put next rectangle");
            }

            Rectangles.Add(nextRectangle);
            return Rectangles.Last();
        }

        private Point GetNextPointForRectangle(Size rectangleSize)
        {
            return pointsGenerator.GetSpiralPoints()
                .FirstOrDefault(p => !new Rectangle(p, rectangleSize).IntersectsWithRectangles(Rectangles));
        }
    }
}