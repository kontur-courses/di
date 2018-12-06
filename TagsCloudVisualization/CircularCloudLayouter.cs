using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public IReadOnlyCollection<Rectangle> Rectangles => rectangles.AsReadOnly();
        private readonly Point center;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();
        private readonly IProvider<Point> provider;

        public CircularCloudLayouter(Point center)
        {
            this.center = center;
            provider = new SpiralPointProvider(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width < 0 || rectangleSize.Height < 0)
                throw new ArgumentException("size width and height must be a positive number");
            var rectangle = new Rectangle(
                new Point(center.X - rectangleSize.Width / 2, center.Y - rectangleSize.Height / 2), rectangleSize);
            while (rectangle.IntersectsWith(rectangles))
                rectangle.Location = provider.GetNext();

            rectangles.Add(rectangle);
            return rectangle;
        }
    }
    

}