using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Layout
{
    public class CircularCloudLayout : IRectangleLayout
    {
        private readonly List<Rectangle> rectangles;
        private readonly Spiral spiral;
        private readonly Size cloudSize;

        public Point Center => spiral.Center;
        public double Area => Math.Pow(spiral.Radius, 2) * Math.PI;
        public IEnumerable<Rectangle> Rectangles => rectangles.ToImmutableList();

        public CircularCloudLayout(int locationX, int locationY, int width, int height)
            : this(new Point(locationX, locationY), new Size(width, height))
        {
        }

        public CircularCloudLayout(Point center, Size size)
        {
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException(nameof(size));

            spiral = new Spiral(center);
            rectangles = new List<Rectangle>();
            cloudSize = size;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height > cloudSize.Height || rectangleSize.Width > cloudSize.Width)
                throw new ArgumentException("Should be less than CloudSize", nameof(rectangleSize));

            Rectangle rectangle;
            do
            {
                var location = spiral.GetNextLocation();
                rectangle = new Rectangle((int) location.X, (int) location.Y, rectangleSize.Width,
                    rectangleSize.Height);

            } while (rectangles.Any(rectangle.IntersectsWith));

            if (rectangle.Location != Center)
                rectangle = ShiftToTheCenter(rectangle);

            rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle ShiftToTheCenter(Rectangle rectangle)
        {
            var currentShiftedRectangle = rectangle;
            Rectangle previousShiftedRectangle;

            do
            {
                previousShiftedRectangle = currentShiftedRectangle;

                var newLocation = MoveLocationToCenter(Center, currentShiftedRectangle.Location);
                currentShiftedRectangle = new Rectangle(newLocation, rectangle.Size);

            } while (!rectangles.Any(currentShiftedRectangle.IntersectsWith));

            return previousShiftedRectangle;
        }

        private static Point MoveLocationToCenter(Point center, Point location)
        {
            var result = location;

            result.X -= result.X.CompareTo(center.X);
            result.Y -= result.Y.CompareTo(center.Y);

            return result;
        }
    }
}