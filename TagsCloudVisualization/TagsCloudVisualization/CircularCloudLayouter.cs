using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ILayouter
    {
        public Point Center => pointLocator.Center;

        private readonly ICirclePointLocator pointLocator;

        private readonly List<Rectangle> taggedRectangles;

        public CircularCloudLayouter(ICirclePointLocator pointLocator)
        {
            taggedRectangles = new List<Rectangle>();
            this.pointLocator = pointLocator;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException();
            var rectangle = CreateRectangleOnSpiral(rectangleSize);
            taggedRectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle CreateRectangleOnSpiral(Size rectangleSize)
        {
            var shiftedCenter = Geometry.ShiftPointBySizeOffsets(pointLocator.Center, rectangleSize);
            var rectangle = new Rectangle(shiftedCenter, rectangleSize);

            while (taggedRectangles.Any(otherRectangle => rectangle.IntersectsWith(otherRectangle)))
            {
                var locatedPoint = pointLocator.GetNextPoint();
                rectangle.X = shiftedCenter.X + locatedPoint.X;
                rectangle.Y = shiftedCenter.Y + locatedPoint.Y;
            }

            AlignLocatorDirection(rectangle);
            return rectangle;
        }

        private void AlignLocatorDirection(Rectangle rectangle)
        {
            pointLocator.DistanceFromCenter -= Math.Max(pointLocator.DistanceFromCenter / 2,
                Geometry.GetLengthFromRectangleCenterToBorderOnVector(rectangle, pointLocator.Center));
        }
    }
}