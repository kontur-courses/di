using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudGenerator.Core.Spirals;
using TagsCloudGenerator.Infrastructure;

namespace TagsCloudGenerator.Core.Layouters
{
    public class SpiralRectangleCloudLayouter : IRectangleCloudLayouter
    {
        private readonly Point center;
        private readonly ISpiral spiral;
        private readonly List<Rectangle> rectangles;

        public SpiralRectangleCloudLayouter(ISpiral spiral)
        {
            this.spiral = spiral;
            center = new Point(0, 0);
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rect = GetNextRectangle(rectangleSize);
            var shiftedRect = ShiftRectangleToCenter(rect);
            rectangles.Add(shiftedRect);
            return shiftedRect;
        }

        private Rectangle GetNextRectangle(Size rectangleSize)
        {
            Rectangle possibleRect;
            do
            {
                var topLeftCornerLocation = GetTopLeftCornerLocationOfRectangle(
                    Point.Round(spiral.GetNextPoint()), rectangleSize);
                possibleRect = new Rectangle(topLeftCornerLocation, rectangleSize);
            } while (IsIntersectsWithOthersRectangles(possibleRect));

            return possibleRect;
        }

        private bool IsIntersectsWithOthersRectangles(Rectangle rect)
        {
            return rectangles.Any(other => Rectangle.Intersect(rect, other) != Rectangle.Empty);
        }

        private Point GetTopLeftCornerLocationOfRectangle(Point centerRectangle, Size rectangleSize)
        {
            return new Point(
                centerRectangle.X - rectangleSize.Width / 2,
                centerRectangle.Y - rectangleSize.Height / 2);
        }

        private Rectangle ShiftRectangleToCenter(Rectangle rect)
        {
            var minDistanceSquare = double.MaxValue;
            var shiftedRect = rect;
            var queue = new Queue<Rectangle>();
            queue.Enqueue(rect);
            while (queue.Count != 0)
            {
                var currentRect = queue.Dequeue();
                var squareDistanceToCenter = currentRect.DistanceToPoint(center);
                if (squareDistanceToCenter >= minDistanceSquare || IsIntersectsWithOthersRectangles(currentRect))
                {
                    continue;
                }
                minDistanceSquare = squareDistanceToCenter;
                shiftedRect = currentRect;
                foreach (var adjacentRectangle in GetAdjacentRectangles(currentRect))
                    queue.Enqueue(adjacentRectangle);
            }

            return shiftedRect;
        }
         
        private IEnumerable<Rectangle> GetAdjacentRectangles(Rectangle rect)
        {
            
            yield return new Rectangle(new Point(rect.X - 1, rect.Y), rect.Size);
            yield return new Rectangle(new Point(rect.X + 1, rect.Y), rect.Size);
            yield return new Rectangle(new Point(rect.X, rect.Y - 1), rect.Size);
            yield return new Rectangle(new Point(rect.X, rect.Y + 1), rect.Size);
        }
    }
}