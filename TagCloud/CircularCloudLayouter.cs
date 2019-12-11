using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class CircularCloudLayouter : ICircularCloudLayouter
    {
        private readonly IAlgorithm algorithm;

        public CircularCloudLayouter(IAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            Rectangles = new List<RectangleF>();
        }

        private List<RectangleF> Rectangles { get; set; }

        public void Clear()
        {
            Rectangles = new List<RectangleF>();
        }

        public RectangleF PutNextRectangle(SizeF rectangleSize, Point center)
        {
            var points = algorithm.GetCoordinates();
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Width and Height should be greater than zero");
            foreach (var point in points)
            {
                var location = GetRectangleLocation(center, rectangleSize, point);
                var rectangle = new RectangleF(location, rectangleSize);
                if (Rectangles.Any(rec => rec.IntersectsWith(rectangle))) continue;
                Rectangles.Add(rectangle);
                return rectangle;
            }

            throw new Exception("Алгоритм работает не правильно");
        }

        private Point GetRectangleLocation(Point center, SizeF rectangleSize, Point point)
        {
            return new Point(point.X + center.X - (int) rectangleSize.Width / 2,
                point.Y + center.Y - (int) rectangleSize.Height / 2);
        }
    }
}