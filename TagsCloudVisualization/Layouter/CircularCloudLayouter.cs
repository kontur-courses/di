using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualization.Layouter
{
    internal class CircularCloudLayouter : ILayouter
    {
        private readonly IPointGenerator generator;
        private readonly List<Rectangle> rectangles;

        public CircularCloudLayouter(IPointGenerator generator)
        {
            this.generator = generator;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0)
                throw new ArgumentException("Rectangle width should be > 0");
            if (rectangleSize.Height <= 0)
                throw new ArgumentException("Rectangle height should be > 0");
            var rectangle = GetCorrectRectangle(rectangleSize);
            rectangles.Add(rectangle);

            return rectangle;
        }

        private Rectangle GetCorrectRectangle(Size size)
        {
            Rectangle rectangle;
            do
            {
                var point = generator.GetNextPoint();
                var location = new Point(point.X - size.Width / 2, point.Y - size.Height / 2);
                rectangle = new Rectangle(location, size);
            } while (rectangle.IntersectsWith(rectangles));

            return rectangle;
        }
    }
}