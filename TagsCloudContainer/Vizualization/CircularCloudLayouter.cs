using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace TagsCloudContainer
{
    public class CircularCloudLayouter
    {
        private readonly List<Rectangle> rectangles = new List<Rectangle>();
        private readonly Point center;
        private readonly ArchimedeanSpiral archimedeanSpiral;

        public CircularCloudLayouter(Point center)
        {
            this.center = center;
            archimedeanSpiral = new ArchimedeanSpiral(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var point = archimedeanSpiral.GetNextPoint();
            var checkedRectangle = new Rectangle(point, rectangleSize);
            while (!IsCorrectToPlace(checkedRectangle))
            {
                point = archimedeanSpiral.GetNextPoint();
                checkedRectangle = new Rectangle(point, rectangleSize);
            }

            var adjustedRectangle = AdjustRectangle(checkedRectangle);
            rectangles.Add(adjustedRectangle);
            return adjustedRectangle;
        }

        public List<Rectangle> GetRectangles()
        {
            return rectangles
                .Select(rectangle => new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height))
                .ToList();
        }

        private Rectangle AdjustRectangle(Rectangle rectangle)
        {
            rectangle = MoveRectangleHorizontally(rectangle);
            rectangle = MoveRectangleVertically(rectangle);
            return rectangle;
        }

        private Rectangle MoveRectangleHorizontally(Rectangle rectangle)
        {
            var stepSize = rectangle.X < center.X ? 1 : -1;
            var checkedRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            while (IsCorrectToPlace(checkedRectangle) && checkedRectangle.X != center.X)
            {
                checkedRectangle.X += stepSize;
            }

            if (!IsCorrectToPlace(checkedRectangle))
            {
                checkedRectangle.X -= stepSize;
            }

            return checkedRectangle;
        }

        private Rectangle MoveRectangleVertically(Rectangle rectangle)
        {
            var stepSize = rectangle.Y < center.Y ? 1 : -1;
            var checkedRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            while (IsCorrectToPlace(checkedRectangle) && checkedRectangle.Y != center.Y)
            {
                checkedRectangle.Y += stepSize;
            }

            if (!IsCorrectToPlace(checkedRectangle))
            {
                checkedRectangle.Y -= stepSize;
            }

            return checkedRectangle;
        }

        private bool IsCorrectToPlace(Rectangle checkedRectangle)
        {
            if (checkedRectangle.X < 0 || checkedRectangle.Y < 0)
            {
                return false;
            }

            return rectangles.All(rectangle => !rectangle.IntersectsWith(checkedRectangle));
        }
    }
}