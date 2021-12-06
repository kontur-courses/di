using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly ArchimedeanSpiralPointGenerator archimedeanSpiralPointGenerator;
        private readonly List<Rectangle> rectangles;

        public CircularCloudLayouter(Point center)
        {
            archimedeanSpiralPointGenerator = new ArchimedeanSpiralPointGenerator(center);
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
                var point = archimedeanSpiralPointGenerator.GetNextPoint();
                var location = new Point(point.X - size.Width / 2, point.Y - size.Height / 2);
                rectangle = new Rectangle(location, size);
            } while (rectangle.IntersectsWith(rectangles));
            
            return rectangle;
        }
    }
}