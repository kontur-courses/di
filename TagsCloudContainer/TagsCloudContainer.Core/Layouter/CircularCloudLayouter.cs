using System.Drawing;
using TagsCloudContainer.Core.Layouter.Enums;
using TagsCloudContainer.Core.Layouter.Interfaces;

namespace TagsCloudContainer.Core.Layouter
{
    public class CircularCloudLayouter : ILayouter
    {
        private Point center;
        private readonly List<Rectangle> _rectangles;
        private readonly List<Direction> _directions;

        public CircularCloudLayouter()
        {
            center = new Point();
            _rectangles = new List<Rectangle>();
            _directions = new List<Direction> { Direction.Top, Direction.Right, Direction.Bottom, Direction.Left };
        }
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Rectangle size is not positive.");

            Rectangle rectangle;
            if (_rectangles.Count == 0)
            {
                rectangle = new Rectangle
                {
                    Size = rectangleSize,
                    Location = new Point(center.X - rectangleSize.Width / 2, center.Y - rectangleSize.Height / 2)
                };
            }
            else
                rectangle = GetRectangle(rectangleSize);

            _rectangles.Add(rectangle);
            return rectangle;
        }

        public void SetCenter(Point center)
        {
            this.center = center;
        }
        private Rectangle GetRectangle(Size rectangleSize)
        {
            var bestDistance = double.MaxValue;
            var bestRectangle = new Rectangle();

            foreach (var rectangle in _rectangles)
            {
                foreach (var direction in _directions)
                {
                    var newRectangle = new Rectangle
                    {
                        Size = rectangleSize,
                        Location = CalculateRectangleLocation(rectangle, rectangleSize, direction)
                    };
                    var distance = GetDistanceToCenter(newRectangle);

                    if (distance < bestDistance && !IntersectsWithOtherRectangles(newRectangle))
                    {
                        bestDistance = distance;
                        bestRectangle = newRectangle;
                    }
                }
            }

            return bestRectangle;
        }

        private double GetDistanceToCenter(Rectangle rectangle)
        {
            var rectangleCenter = new Point(rectangle.X + rectangle.Width / 2,
                                            rectangle.Y + rectangle.Height / 2);

            return GetDistance(rectangleCenter, center);
        }

        private bool IntersectsWithOtherRectangles(Rectangle rectangle)
        {
            return _rectangles.Any(previous => previous.IntersectsWith(rectangle));
        }

        private static Point GetOffset(Size rectangleSize, Rectangle previous, Direction direction)
        {
            return direction switch
            {
                Direction.Top => new Point(0, previous.Height),                
                Direction.Left => new Point(-rectangleSize.Width, 0),
                Direction.Right => new Point(previous.Width, previous.Height - rectangleSize.Height),
                Direction.Bottom => new Point(previous.Width - rectangleSize.Width, -rectangleSize.Height),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static Point CalculateRectangleLocation(Rectangle previous, Size rectangleSize, Direction direction)
        {
            var offset = GetOffset(rectangleSize, previous, direction);
            return new Point(previous.X + offset.X, previous.Y + offset.Y);
        }

        private static double GetDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}
