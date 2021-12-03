using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public Cloud Cloud { get; }
        private readonly Spiral spiral;

        public CircularCloudLayouter(Point center)
        {
            Cloud = new Cloud(center);
            spiral = Spiral.Create(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0
                || rectangleSize.Height <= 0)
                throw new ArgumentException("Size values must be positive numbers");

            var rectangle = GetNextRectangle(rectangleSize);
            rectangle = Optimize(rectangle);
            Cloud.Rectangles.Add(rectangle);

            return rectangle;
        }

        public void PutManyRectangles(int count, Random random,
            int minSize, int maxSize)
        {
            for (var i = 0; i < count; i++)
                PutNextRectangle(new Size(
                        random.Next(minSize, maxSize),
                        random.Next(minSize, maxSize)));
        }

        private Rectangle GetNextRectangle(Size rectangleSize)
        {
            var rectangle = Rectangle.Empty;
            while (IsIncorrectLocation(rectangle))
            {
                var location = spiral.GetNext() - rectangleSize / 2;
                rectangle = new Rectangle(location, rectangleSize);
            }
            return rectangle;
        }

        private Rectangle Optimize(Rectangle rectangle)
        {
            if (Cloud.Rectangles.Count == 0)
                return rectangle;

            rectangle = GetOptimizedRectangle(rectangle);

            return rectangle;
        }

        private Rectangle GetOptimizedRectangle(Rectangle rectangle)
        {
            var distance = Cloud.Center
                .GetDistance(rectangle.Location + rectangle.Size / 2);

            while (true)
            {
                var newLocation = new Point(CalculateOptimizedPosition(true, rectangle),
                    CalculateOptimizedPosition(false, rectangle));

                var newDistance = Cloud.Center
                    .GetDistance(newLocation + rectangle.Size / 2);
                var newRectangle = new Rectangle(newLocation, rectangle.Size);

                if (newDistance >= distance
                    || IsIncorrectLocation(newRectangle))
                    break;

                rectangle = newRectangle;
                distance = newDistance;
            }

            return rectangle;
        }

        private bool IsIncorrectLocation(Rectangle rectangle)
        {
            return rectangle.IsEmpty || Cloud.Rectangles.Any(rectangle.IntersectsWith);
        }

        private int CalculateOptimizedPosition(bool isX, Rectangle rectangle)
        {
            if (isX)
                return rectangle.Location.X
                    + Math.Sign(Cloud.Center.X
                    - rectangle.Location.X - rectangle.Size.Width / 2);
            return rectangle.Location.Y
                + Math.Sign(Cloud.Center.Y
                - rectangle.Location.Y - rectangle.Size.Height / 2);
        }
    }
}
