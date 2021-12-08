using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.CloudLayouter.VectorsGenerator;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.CloudLayouter
{
    public class NonIntersectedLayouter : ILayouter
    {
        private readonly Point _center;
        private readonly List<Rectangle> _rectangles = new();
        private readonly IVectorsGenerator _vectorsGenerator;

        public NonIntersectedLayouter(Point center, IVectorsGenerator vectorsGenerator)
        {
            _center = center;
            _vectorsGenerator = vectorsGenerator ?? throw new ArgumentNullException(nameof(vectorsGenerator));
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0) throw new ArgumentException("Width should be positive");
            if (rectangleSize.Height <= 0) throw new ArgumentException("Height should be positive");

            var rectangle = CreateCorrectRectangle(rectangleSize);
            _rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle CreateCorrectRectangle(Size rectangleSize)
        {
            var rectangle = new Rectangle(_center, rectangleSize);
            while (true)
            {
                var vector = _vectorsGenerator.GetNextVector();
                rectangle.X = _center.X + vector.X - rectangleSize.Width / 2;
                rectangle.Y = _center.Y + vector.Y - rectangleSize.Height / 2;
                if (!_rectangles.Any(x => x.IntersectsWith(rectangle))) return GetShiftedToCenterRectangle(rectangle);
            }
        }

        private Rectangle GetShiftedToCenterRectangle(Rectangle rectangle)
        {
            var center = rectangle.GetCenter();
            var shiftX = new Size(Math.Sign(_center.X - center.X), 0);
            var shiftY = new Size(0, Math.Sign(_center.Y - center.Y));

            if (!shiftX.IsEmpty)
                rectangle = ShiftUntilIntersection(rectangle,
                    rect => new Rectangle(rect.Location + shiftX, rect.Size),
                    rect => IsRectangleAtCenterAxis(rect, point => point.X));

            if (!shiftY.IsEmpty)
                rectangle = ShiftUntilIntersection(rectangle,
                    rect => new Rectangle(rect.Location + shiftY, rect.Size),
                    rect => IsRectangleAtCenterAxis(rect, point => point.Y));

            return rectangle;
        }

        private Rectangle ShiftUntilIntersection(Rectangle rectangle, Func<Rectangle, Rectangle> shift,
            Func<Rectangle, bool> isAtCenterAxis)
        {
            var shifted = rectangle;

            while (!isAtCenterAxis(shifted))
            {
                var temp = shift(shifted);
                if (_rectangles.Any(rect => rect.IntersectsWith(temp))) break;
                shifted = temp;
            }

            return shifted;
        }

        private bool IsRectangleAtCenterAxis(Rectangle rectangle, Func<Point, int> getAxis)
        {
            var rectangleCenter = rectangle.GetCenter();
            return Math.Abs(getAxis(_center) - getAxis(rectangleCenter)) == 0;
        }
    }
}