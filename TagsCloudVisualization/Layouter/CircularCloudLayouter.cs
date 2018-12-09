using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Layouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<Rectangle> rectangles;

        private readonly Spiral spiral;

        public CircularCloudLayouter(Spiral spiral)
        {
            this.spiral = spiral;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Size should be positive");
            var nextRectangle = GenerateNextRectangle(rectangleSize);
            rectangles.Add(nextRectangle);
            return nextRectangle;
        }


        private Rectangle GenerateNextRectangle(Size rectangleSize)
        {
            while (true)
            {
                var rectangleCenter = spiral.GetNextPoint();
                var nexRectangle = new Rectangle(rectangleCenter, rectangleSize)
                    .ShiftRectangleToTopLeftCorner();
                if (!rectangles.Any(nexRectangle.IntersectsWith))
                    return nexRectangle;
            }
        }
    }
}
