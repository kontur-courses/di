using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.Logic
{
    public class CircularCloudLayouter : ILayouter
    {
        private readonly ICirclePointLocator pointLocator;
        private List<Rectangle> taggedRectangles;

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

        public void Reset()
        {
            taggedRectangles = new List<Rectangle>();
            pointLocator.DistanceFromCenter = 0;
            pointLocator.Angle = 0;
        }

        private Rectangle CreateRectangleOnSpiral(Size rectangleSize)
        {
            var shiftedCenter = Geometry.ShiftPointBySizeOffsets(Point.Empty, rectangleSize);
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
                Geometry.GetLengthFromRectangleCenterToBorderOnVector(rectangle, Point.Empty));
        }
    }
}