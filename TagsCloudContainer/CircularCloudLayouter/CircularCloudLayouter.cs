using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer.CircularCloudLayouter
{
    public class CircularCloudLayouter
    {
        private readonly ArchimedesSpiral archimedesSpiral;

        public List<Rectangle> Rectangles { get; }
        public Point Center { get; }

        public CircularCloudLayouter(Point center)
        {
            this.Center = center;
            archimedesSpiral = new ArchimedesSpiral(center, 0.1);
            Rectangles = new List<Rectangle>();
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
            var shouldPushByX = true;
            var shouldPushByY = true;
            var lastNonZeroXOffset = 0;
            var lastNonZeroYOffset = 0;
            while (shouldPushByX || shouldPushByY)
            {
                var dx = 0;
                var dy = 0;
                
                dx = GetRelativeRectangleOffsetDeltaX(rectangle);
                if (dx != 0)
                {
                    lastNonZeroXOffset = dx;
                    shouldPushByX = true;
                }
                else
                    shouldPushByX = false;

                dy = GetRelativeRectangleOffsetDeltaY(rectangle);
                if (dy != 0)
                {
                    lastNonZeroYOffset = dy;
                    shouldPushByY = true;
                }
                else
                    shouldPushByY = false;

                rectangle.Offset(dx, dy);
                
                if (!rectangle.IntersectsWithAny(Rectangles)) continue;
                rectangle.Offset(-lastNonZeroXOffset, -lastNonZeroYOffset);
                break;
            }
            return rectangle;
        }

        private int GetRelativeRectangleOffsetDeltaX(Rectangle rectangle)
        {
            var rectangleCenter = rectangle.GetCenter();
            var xDirection = rectangleCenter.X >= Center.X ? rectangleCenter.X == Center.X ? 0 : -1 : 1;
            var shouldPushByX = ShouldPushRectangleByX(rectangle, xDirection);
            return shouldPushByX ? xDirection : 0;
        }
        
        private int GetRelativeRectangleOffsetDeltaY(Rectangle rectangle)
        {
            var rectangleCenter = rectangle.GetCenter();
            var yDirection = rectangleCenter.Y >= Center.Y ? rectangleCenter.Y == Center.Y ? 0 : -1 : 1;
            var shouldPushByY = ShouldPushRectangleByY(rectangle, yDirection);
            return shouldPushByY ? yDirection : 0;
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