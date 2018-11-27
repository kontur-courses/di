using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    /// <summary>
    /// Put rectangles in cloud in order of distance from center
    /// </summary>
    class CloudLayouter
    {
        public CloudLayouter(ISpiral spiral, Point center)
        {
            Rectangles = new List<Rectangle>();
            Center = center;
            this.spiral = spiral;
        }

        enum Direction { Up, Down, Left, Right };
        private readonly ISpiral spiral;

        public Point Center { get; }
        public List<Rectangle> Rectangles { get; }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = spiral.GetRectangleInNextLocation(rectangleSize);
            while (IntersectsWithPrevious(rectangle))
                rectangle = spiral.GetRectangleInNextLocation(rectangleSize);

            if (Rectangles.Count != 0)
            {
                if (Center.X > rectangle.X)
                    OffsetRectangle(rectangle, Direction.Right);

                if (Center.X > rectangle.X)
                    OffsetRectangle(rectangle, Direction.Left);

                if (Center.Y > rectangle.Y)
                    OffsetRectangle(rectangle, Direction.Down);

                if (Center.Y < rectangle.Y)
                    OffsetRectangle(rectangle, Direction.Up);
            }

            Rectangles.Add(rectangle);
            return rectangle;
        }

        private void OffsetRectangle(Rectangle rectangle, Direction direction)
        {
            var counter = 0;
            var offset = new Point(0, 0);

            switch (direction)
            {
                case Direction.Right:
                    offset = new Point(0, 1);
                    counter = Center.X - rectangle.GetCenter().X;
                    break;
                case Direction.Left:
                    offset = new Point(0, -1);
                    counter = rectangle.GetCenter().X - Center.X;
                    break;
                case Direction.Up:
                    offset = new Point(-1, 0);
                    counter = rectangle.GetCenter().Y - Center.Y;
                    break;
                case Direction.Down:
                    offset = new Point(1, 0);
                    counter = Center.Y - rectangle.GetCenter().Y;
                    break;
            }

            while (!IntersectsWithPrevious(rectangle) && counter > 0)
            {
                var location = rectangle.Location;
                location.Offset(offset);
                rectangle = new Rectangle(location, rectangle.Size);   
                counter--;
            }
        }

        private bool IntersectsWithPrevious(Rectangle rectangle)
        {
            return Rectangles.Any(rectangle.IntersectsWith);
        }
    }
}

