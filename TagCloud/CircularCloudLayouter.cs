using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;


namespace TagCloud
{
    public class CircularCloudLayouter : ICircularCloudLayouter
    {
        private IAlgorithm Algorithm { get; }
        public List<Rectangle> Rectangles { get; }
        public CircularCloudLayouter(IAlgorithm algorithm)
        {
            Algorithm = algorithm;
            Rectangles = new List<Rectangle>();
        }
        public Rectangle PutNextRectangle(Size rectangleSize, Point center)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Width and Height should be greater than zero");
            while (true)
            {
                var point = new Point(Algorithm.GetNextCoordinate().X + center.X - rectangleSize.Width / 2,
                        Algorithm.GetNextCoordinate().Y + center.Y - rectangleSize.Height / 2);
                var rectangle = new Rectangle(point, rectangleSize);
                if (Rectangles.Any(rec => rec.IntersectsWith(rectangle))) continue;
                Rectangles.Add(rectangle);
                return rectangle;
            }
        }
    }
}
