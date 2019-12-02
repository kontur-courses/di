using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer.CloudLayouters.CircularCloudLayouter
{
    public class CircularCloudLayouter : ICloudLayoutingAlgorithm
    {
        private readonly ArchimedesSpiral archimedesSpiral;

        public List<Rectangle> Rectangles { get; }
        public Point Center { get; }

        private readonly int broadness;

        public CircularCloudLayouter(Point center, double step, int broadness)
        {
            Center = center;
            archimedesSpiral = new ArchimedesSpiral(center, step);
            Rectangles = new List<Rectangle>();
            this.broadness = broadness;
        }
        
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if(rectangleSize.IsEmpty)
                throw new ArgumentException("Empty rectangle size");
            var rectangle = GetNextRectangle(rectangleSize);
            Rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GetNextRectangle(Size rectangleSize)
        {
            var rectangleToPlace = default(Rectangle);
            foreach (var point in archimedesSpiral.GetSpiralPoints())
            {
                var possibleRectangle = new Rectangle(point, rectangleSize).GetCentered(point);
                if (possibleRectangle.IntersectsWithAny(Rectangles)) continue;
                rectangleToPlace = GetRectanglePushedCloserToCenter(possibleRectangle);
                break;
            }
            return rectangleToPlace;
        }

        private Rectangle GetRectanglePushedCloserToCenter(Rectangle rectangle)
        {
            while (true)
            {
                var dx = GetRelativeRectangleOffsetDeltaX(rectangle);
                var dy = GetRelativeRectangleOffsetDeltaY(rectangle);
                if(dx == 0 && dy == 0)
                    break;
                
                var movedRectangle = new Rectangle(
                    rectangle.X + dx, rectangle.Y + dy, rectangle.Width, rectangle.Height);
                
                if (movedRectangle.IntersectsWithAny(Rectangles)) break;
                rectangle = movedRectangle;
            }
            return rectangle;
        }

        private int GetRelativeRectangleOffsetDeltaX(Rectangle rectangle)
        {
            var rectangleCenter = rectangle.GetCenter();
            var xDirection = rectangleCenter.X >= Center.X ? rectangleCenter.X == Center.X ? 0 : -1 : 1;
            var shouldPushByX = ShouldPushRectangleByX(rectangle, xDirection);
            return shouldPushByX ? broadness * xDirection : 0;
        }
        
        private int GetRelativeRectangleOffsetDeltaY(Rectangle rectangle)
        {
            var rectangleCenter = rectangle.GetCenter();
            var yDirection = rectangleCenter.Y >= Center.Y ? rectangleCenter.Y == Center.Y ? 0 : -1 : 1;
            var shouldPushByY = ShouldPushRectangleByY(rectangle, yDirection);
            return shouldPushByY ? broadness * yDirection : 0;
        }

        private bool ShouldPushRectangleByX(Rectangle rectangle, int dx)
        {
            rectangle.Offset(dx, 0);
            return !rectangle.IntersectsWithAny(Rectangles) && rectangle.GetCenter().X != Center.X;
        }
        
        private bool ShouldPushRectangleByY(Rectangle rectangle, int dy)
        {
            rectangle.Offset(0, dy);
            return !rectangle.IntersectsWithAny(Rectangles) && rectangle.GetCenter().Y != Center.Y;
        }
    }
}