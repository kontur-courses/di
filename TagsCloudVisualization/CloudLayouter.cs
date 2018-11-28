using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    /// <summary>
    /// Put rectangles in cloud in order of distance from Center
    /// </summary>
    public partial class CloudLayouter : ICloudLayouter
    {
        public CloudLayouter(ISpiral spiral)
        {
            Rectangles = new List<Rectangle>();
            Spiral = spiral;
        }

        public ISpiral Spiral { get; }
        
        public List<Rectangle> Rectangles { get; }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = Spiral.GetRectangleInNextLocation(rectangleSize);
            while (IntersectsWithPrevious(rectangle))
                rectangle = Spiral.GetRectangleInNextLocation(rectangleSize);

            if (Rectangles.Count != 0)
            {
                if (Spiral.Center.X > rectangle.X)
                    OffsetRectangle(rectangle, Direction.Right);

                if (Spiral.Center.X > rectangle.X)
                    OffsetRectangle(rectangle, Direction.Left);

                if (Spiral.Center.Y > rectangle.Y)
                    OffsetRectangle(rectangle, Direction.Down);

                if (Spiral.Center.Y < rectangle.Y)
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
                    counter = Spiral.Center.X - rectangle.GetCenter().X;
                    break;
                case Direction.Left:
                    offset = new Point(0, -1);
                    counter = rectangle.GetCenter().X - Spiral.Center.X;
                    break;
                case Direction.Up:
                    offset = new Point(-1, 0);
                    counter = rectangle.GetCenter().Y - Spiral.Center.Y;
                    break;
                case Direction.Down:
                    offset = new Point(1, 0);
                    counter = Spiral.Center.Y - rectangle.GetCenter().Y;
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

