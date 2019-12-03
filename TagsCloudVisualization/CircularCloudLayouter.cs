using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    class CircularCloudLayouter
    {
        public readonly Point Center;
        public List<Rectangle> Rectangles { get; }
        private readonly SpiralGenerator generator;

        public CircularCloudLayouter(Point center)
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException();
            Center = center;
            Rectangles = new List<Rectangle>();
            generator = new SpiralGenerator(Center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException();

            var newRectangle = PutRectangleOnSpiral(rectangleSize);
            while (newRectangle.IntersectsWith(Rectangles))
            {
                newRectangle = PutRectangleOnSpiral(rectangleSize);
            }

            newRectangle = MoveRectangleToCenter(newRectangle);
            Rectangles.Add(newRectangle);
            return newRectangle;
        }

        private Rectangle PutRectangleOnSpiral(Size rectangleSize)
        {
            var point = generator.GetNextPoint();
            return new Rectangle(new Point(point.X - rectangleSize.Width / 2, point.Y - rectangleSize.Height / 2), rectangleSize);
        }

        private Rectangle MoveRectangleToCenter(Rectangle rectangle)
        {
            while (true)
            {
                var direction = Center - (Size) rectangle.GetCenter();

                var newRectangle = TryOffsetInDirection(rectangle, new Point(Math.Sign(direction.X), 0));
                newRectangle = TryOffsetInDirection(newRectangle, new Point(0, Math.Sign(direction.Y)));
                if (newRectangle == rectangle)
                {
                    break;
                }
                rectangle = newRectangle;
            }

            return rectangle;
        }

        private Rectangle TryOffsetInDirection(Rectangle rectangle, Point direction)
        {
            var newRectangle = new Rectangle(rectangle.Location, rectangle.Size);
            newRectangle.Offset(direction);
            return newRectangle.IntersectsWith(Rectangles) ? rectangle : newRectangle;
        }
    }
}
