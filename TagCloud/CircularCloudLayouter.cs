using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class CircularCloudLayouter
    {
        internal readonly HashSet<Rectangle> rectangles = new HashSet<Rectangle>();
        public readonly Point center;
        private readonly IPointGetter getPointer;
        public int Top { get; private set; }
        public int Bottom { get; private set; }
        public int Right { get; private set; }
        public int Left { get; private set; }

        public bool IsEmpty => !rectangles.Any();

        public Size Size => new Size(Right - Left, Bottom - Top);
        internal CircularCloudLayouter(Point center)
        {
            this.center = center;
            getPointer = new PointGetter(center);
            Top = center.Y;
            Bottom = center.Y;
            Right = center.X;
            Left = center.X;
        }

        internal Rectangle PutNextRectangle(Size rectangleSize)
        {
            var result = MoveToCentre(PutRectangleOnCircle(rectangleSize));
            ChangeSize(result);
            rectangles.Add(result);
            return result;
        }

        private void ChangeSize(Rectangle rectangle)
        {
            if (IsEmpty)
            {
                Top = rectangle.Top;
                Bottom = rectangle.Bottom;
                Right = rectangle.Right;
                Left = rectangle.Left;
            }
            if (rectangle.Top < Top)
                Top = rectangle.Top;
            if (rectangle.Bottom > Bottom)
                Bottom = rectangle.Bottom;
            if (rectangle.Left < Left)
                Left = rectangle.Left;
            if (rectangle.Right > Right)
                Right = rectangle.Right;
        }

        private Rectangle PutRectangleOnCircle(Size size)
        {
            Rectangle rectangle;
            do
            {
                var point = getPointer.GetNextPoint();
                rectangle = GetRectangleFromSizeAndCenter(size, point);
            } while (HasIntersection(rectangle));
            return rectangle;
        }

        private Rectangle GetRectangleFromSizeAndCenter(Size size, Point rectangleCenter)
        {
            var location = new Point(rectangleCenter.X - (int)(size.Width / 2), rectangleCenter.Y - (int)(size.Height / 2));
            return new Rectangle(location, size);
        }

        private Rectangle MoveToCentre(Rectangle rectangle)
        {
            var previousRectangle = new Rectangle();
            while (true)
            {
                var vectorToMove = GetVectorToMove(rectangle);
                rectangle = MoveToPixel(rectangle, vectorToMove);
                if (rectangle == previousRectangle)
                    break;
                previousRectangle = rectangle;
            }
            return rectangle;
        }

        private Rectangle MoveToPixel(Rectangle rectangle, Point vectorToMove)
        {
            if (Math.Abs(vectorToMove.X) > Math.Abs(vectorToMove.Y))
            {
                var newRectangleX = new Rectangle(rectangle.Location, rectangle.Size);
                newRectangleX.X += vectorToMove.X > 0 ? 1 : -1;
                if (!HasIntersection(newRectangleX))
                    return newRectangleX;
            }
            var newRectangle = new Rectangle(rectangle.Location, rectangle.Size);
            newRectangle.Y += vectorToMove.Y > 0 ? 1 : vectorToMove.Y < 0 ? -1 : 0;
            if (!HasIntersection(newRectangle))
                return newRectangle;
            return rectangle;
        }

        private bool HasIntersection(Rectangle rectangle) => rectangles.Any(r => r.IntersectsWith(rectangle));

        private Point GetVectorToMove(Rectangle rectangle)
        {
            var centreRectangle = GetCentreRectangle(rectangle);
            return new Point(center.X - centreRectangle.X, center.Y - centreRectangle.Y);
        }

        private Point GetCentreRectangle(Rectangle rectangle)
        {
            var yCentreRectangle = (int)((rectangle.Top + rectangle.Bottom) / 2);
            var xCentreRectangle = (int)((rectangle.Left + rectangle.Right) / 2);
            var centreRectangle = new Point(xCentreRectangle, yCentreRectangle);
            return centreRectangle;
        }
    }
}
