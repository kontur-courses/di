using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class SuperTightCloudLayouter : ICloudLayouter
    {
        public IReadOnlyCollection<Rectangle> Rectangles => rectangles.AsReadOnly();
        private readonly Point center;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();
        private readonly IProvider<Point> provider;

        public SuperTightCloudLayouter(Point center)
        {
            this.center = center;
            provider = new SpiralPointProvider(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width < 0) throw new ArgumentException("width must be a positive number");
            if (rectangleSize.Height < 0) throw new ArgumentException("height must be a positive number");
            var rectangle = new Rectangle(new Point(center.X - rectangleSize.Width / 2, center.Y - rectangleSize.Height / 2), rectangleSize);

            var phi = 0.0;
            while (rectangle.IntersectsWith(rectangles))
            {
                phi += 0.1;
                rectangle.X = center.X + (int)Math.Floor(phi * 0.5 * Math.Cos(phi));
                rectangle.Y = center.Y + (int)Math.Floor(phi * 0.5 * Math.Sin(phi));
            }

            rectangles.Add(rectangle);

            return rectangle;
        }
    }
}
