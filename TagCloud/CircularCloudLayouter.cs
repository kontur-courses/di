using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;


namespace TagCloud
{
    public class CircularCloudLayouter : ICircularCloudLayouter
    {
        private readonly IAlgorithm algorithm;
        private List<Rectangle> rectangles;
        public CircularCloudLayouter(IAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            rectangles = new List<Rectangle>();
        }

        public void Clear()
        {
            rectangles = new List<Rectangle>();
        }
        public Rectangle PutNextRectangle(Size rectangleSize, Point center)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Width and Height should be greater than zero");
            while (true)
            {
                var point = GetRectangleLocation(center, rectangleSize);
                var rectangle = new Rectangle(point, rectangleSize);
                if (rectangles.Any(rec => rec.IntersectsWith(rectangle))) continue;
                rectangles.Add(rectangle);
                return rectangle;
            }
        }

        private Point GetRectangleLocation(Point center,Size rectangleSize)
        {
            return new Point(this.algorithm.GetNextCoordinate().X + center.X - rectangleSize.Width / 2,
                this.algorithm.GetNextCoordinate().Y + center.Y - rectangleSize.Height / 2);
        }
    }
}
