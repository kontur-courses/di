using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer.Core.LayoutAlgorithms
{
    class CircularCloudLayouter : ILayoutAlgorithm
    {
        private readonly Point center;
        private readonly List<Rectangle> rectangles;
        private readonly ArchimedeanSpiral spiral;
        public IEnumerable<Rectangle> Rectangles => rectangles;

        public CircularCloudLayouter(Point center)
        {
            this.center = center;
            rectangles = new List<Rectangle>();
            spiral = new ArchimedeanSpiral(this.center);
        }

        public Size GetLayoutSize()
        {
            var maxOffsetFromCenterAlongAxisX = GetMaxOffsetFromCenterAlongAxis(Axis.X);
            var maxOffsetFromCenterAlongAxisY = GetMaxOffsetFromCenterAlongAxis(Axis.Y);
            var width = 2 * Math.Max(center.X, maxOffsetFromCenterAlongAxisX);
            var height = 2 * Math.Max(center.Y, maxOffsetFromCenterAlongAxisY);
            return new Size(width, height);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = new Rectangle();
            foreach (var point in spiral.GetPoints(50))
            {
                rectangle = new Rectangle(point, rectangleSize);
                if (!DoesItIntersectWithSome(rectangle))
                    break;
            }

            if (rectangles.Count > 0)
                rectangle = ShiftToCenter(rectangle);

            rectangles.Add(rectangle);
            return rectangle;
        }

        public int GetMaxOffsetFromCenterAlongAxis(Axis axis)
        {
            return axis == Axis.X
                ? GetMaxValueAlongAxis(Axis.X) - center.X
                : GetMaxValueAlongAxis(Axis.Y) - center.Y;
        }

        private int GetMaxValueAlongAxis(Axis axis)
        {
            return axis == Axis.X
                ? rectangles.Select(rectangle => rectangle.Right).Max()
                : rectangles.Select(rectangle => rectangle.Bottom).Max();
        }

        private bool DoesItIntersectWithSome(Rectangle rectangle) => rectangles.Any(r => r.IntersectsWith(rectangle));

        private Point GetOffset(Point first, Point second, Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    return first.X - second.X > 0 ? new Point(1, 0) : new Point(-1, 0);
                case Axis.Y:
                    return first.Y - second.Y > 0 ? new Point(0, 1) : new Point(0, -1);
                default:
                    throw new ArgumentException("Axis can only be X or Y");
            }
        }

        private Rectangle ShiftToCenterAlongOneAxis(Rectangle rectangle, Axis axis)
        {
            var offset = GetOffset(center, rectangle.Location, axis);
            var oldRectangle = rectangle;
            var axisCenterValue = center.SelectCoordinatePointAlongAxis(axis);
            var axisRectangleValue = rectangle.Location.SelectCoordinatePointAlongAxis(axis);

            while (!DoesItIntersectWithSome(rectangle) && axisCenterValue != axisRectangleValue)
            {
                oldRectangle = rectangle;
                var newLocation = rectangle.Location.Add(offset);
                rectangle = new Rectangle(newLocation, rectangle.Size);
                axisRectangleValue = rectangle.Location.SelectCoordinatePointAlongAxis(axis);
            }

            return oldRectangle;
        }

        private Rectangle ShiftToCenter(Rectangle rectangle)
        {
            rectangle = ShiftToCenterAlongOneAxis(rectangle, Axis.X);
            return ShiftToCenterAlongOneAxis(rectangle, Axis.Y);
        }
    }
}