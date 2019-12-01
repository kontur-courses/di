using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public Point Center => spiral.Center;

        private readonly ArchimedeanSpiral spiral;

        private readonly List<Rectangle> taggedRectangles;

        public CircularCloudLayouter(Point center)
        {
            taggedRectangles = new List<Rectangle>();
            spiral = new ArchimedeanSpiral(center);
        }

        public IEnumerable<Rectangle> GetRectangles()
        {
            return taggedRectangles;
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
            var shiftedCenter = Geometry.ShiftPointBySizeOffsets(spiral.Center, rectangleSize);
            var rectangle = new Rectangle(shiftedCenter, rectangleSize);

            while (taggedRectangles.Any(otherRectangle => rectangle.IntersectsWith(otherRectangle)))
            {
                var spiralPoint = spiral.GetNextPoint();
                rectangle.X = shiftedCenter.X + spiralPoint.X;
                rectangle.Y = shiftedCenter.Y + spiralPoint.Y;
            }

            AlignSpiralDirection(rectangle);
            return rectangle;
        }

        private void AlignSpiralDirection(Rectangle rectangle)
        {
            spiral.DistanceFromCenter -= Math.Max(spiral.DistanceFromCenter / 2,
                Geometry.GetLengthFromRectangleCenterToBorderOnVector(rectangle, Center));
        }
    }
}