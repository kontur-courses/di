using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;


namespace TagCloud
{
    public class CircularCloudLayouter : ICircularCloudLayouter
    {
        private readonly IAlgorithm algorithm;
        private List<RectangleF> Rectangles { get; set; }
        public CircularCloudLayouter(IAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            Rectangles = new List<RectangleF>();
        }

        public void Clear()
        {
            Rectangles = new List<RectangleF>();
        }
        public RectangleF PutNextRectangle(SizeF rectangleSize, Point center)
        {
            algorithm.Update();
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Width and Height should be greater than zero");
            while (true)
            {
                var point = GetRectangleLocation(center, rectangleSize);
                var rectangle = new RectangleF(point, rectangleSize);
                if (Rectangles.Any(rec => rec.IntersectsWith(rectangle))) continue;
                Rectangles.Add(rectangle);
                return rectangle;
            }
        }

        private Point GetRectangleLocation(Point center,SizeF rectangleSize)
        {
            return new Point(this.algorithm.GetNextCoordinate().X + center.X - (int)rectangleSize.Width / 2,
                this.algorithm.GetNextCoordinate().Y + center.Y - (int)rectangleSize.Height / 2);
        }
    }
}
