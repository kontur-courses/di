using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class CircularCloudLayouter
    {
        public Size Size { get; }
        public Point Center { get; }
        private readonly List<Rectangle> rectangles;
        private readonly Spiral spiral;
        
        public CircularCloudLayouter(Point center)
        {
            Center = center;
            Size = new Size(center.X * 2, center.Y * 2);
            rectangles = new List<Rectangle>();
            spiral = new Spiral(Center);
        }

        public ReadOnlyCollection<Rectangle> GetRectangles() => rectangles.AsReadOnly();
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height < 1 || rectangleSize.Width < 1)
                throw new ArgumentException("Размер прямоугольника должен быть больше 0");

            if (!rectangles.Any())
            {
                var firstRectangleLocation 
                    = new Point(Center.X - rectangleSize.Width / 2, Center.Y - rectangleSize.Height / 2);
                var firstRectangle = new Rectangle(firstRectangleLocation, rectangleSize);
                rectangles.Add(firstRectangle);
                return firstRectangle;
            }
            var rectangle = CreateNewRectangle(rectangleSize);

            while (rectangles.Any(e => e.IntersectsWith(rectangle)))
            {
                rectangle = CreateNewRectangle(rectangleSize);
            }
            rectangle = MoveRectangleCloserToCenter(rectangle);
            rectangles.Add(rectangle);

            return rectangle;
        }

        public Point GetRectangleCenter(Rectangle rect)
        {
            return new Point(rect.Left + rect.Width / 2,
                rect.Top + rect.Height / 2);
        }

        private Rectangle MoveRectangleCloserToCenter(Rectangle rectangle)
        {
            rectangle = MoveRectangleCloserToCenterByY(rectangle);
            return MoveRectangleCloserToCenterByX(rectangle);
        }
        private Rectangle MoveRectangleCloserToCenterByY(Rectangle rectangle)
        {
            while (true)
            {
                var result = rectangle;

                if (GetRectangleCenter(rectangle).Y > Center.Y)
                    rectangle.Y--;

                if (GetRectangleCenter(rectangle).Y < Center.Y)
                    rectangle.Y++;

                if (GetRectangleCenter(rectangle).Y == Center.Y || rectangles.Any(e => e.IntersectsWith(rectangle)))
                    return result;
            }
        }
        private Rectangle MoveRectangleCloserToCenterByX(Rectangle rectangle)
        {
            while (true)
            {
                var result = rectangle;

                if (GetRectangleCenter(rectangle).X > Center.X)
                    rectangle.X--;

                if (GetRectangleCenter(rectangle).X < Center.X)
                    rectangle.X++;

                if(GetRectangleCenter(rectangle).X == Center.X || rectangles.Any(e => e.IntersectsWith(rectangle)))
                    return result;

            }
        }

        private Rectangle CreateNewRectangle(Size rectangleSize)
        {
            var rectangleLocation = spiral.GetNextPoint();
            var newRectangle = new Rectangle(rectangleLocation, rectangleSize);
            return newRectangle;
        }
    }
}
