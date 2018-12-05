using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<Rectangle> rectangles;

        public Point Center { get; }

        private Spiral Spiral { get; }

        public int Radius => rectangles.GetSurroundingCircleRadius();

        public CircularCloudLayouter(LayouterSettings layouterSettings)
        {
            Center = layouterSettings.Center;
            Spiral = layouterSettings.Spiral;
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
            if (rectangles.Any())
            {
                while (true)
                {
                    var rectangleCenter = Spiral.GetNextPoint(Center);
                    var nexRectangle = new Rectangle(rectangleCenter, rectangleSize)
                        .ShiftRectangleToTopLeftCorner();
                    if (!rectangles.Any(nexRectangle.IntersectsWith))
                        return nexRectangle;
                }
            }
            return new Rectangle(Center, rectangleSize).ShiftRectangleToTopLeftCorner();
        }
    }
}
