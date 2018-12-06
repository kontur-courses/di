using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<Rectangle> rectangles;

        private readonly IPolar polar;

        public CircularCloudLayouter(IPolar polar)
        {
            this.polar = polar;
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
            if (rectangles.Count == 0) 
                return new Rectangle(polar.Center, rectangleSize).ShiftRectangleToTopLeftCorner();
            while (true)
            {
                var rectangleCenter = polar.GetNextPoint();
                var nexRectangle = new Rectangle(rectangleCenter, rectangleSize)
                    .ShiftRectangleToTopLeftCorner();
                if (!rectangles.Any(nexRectangle.IntersectsWith))
                    return nexRectangle;
            }
        }
    }
}
