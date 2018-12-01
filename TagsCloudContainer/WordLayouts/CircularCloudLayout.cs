using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.WordLayouts
{
    public class CircularCloudLayout : IWordLayout
    {
        private readonly List<RectangleF> rectangles;
        private readonly PointF centerPoint;
        private readonly double angleDelta;
        private double angle;

        public CircularCloudLayout(CircularCloudLayoutConfig config)
        {
            rectangles = new List<RectangleF>();
            centerPoint = config.CenterPoint;
            angleDelta = config.AngleDelta;
        }

        private IEnumerable<PointF> GetPoints()
        {
            while (true)
            {
                if (rectangles.Count == 0)
                {
                    yield return centerPoint;
                }

                var angleInRadians = angle * Math.PI / 180.0;
                var x = centerPoint.X + angleInRadians * Math.Cos(angleInRadians);
                var y = centerPoint.Y + angleInRadians * Math.Sin(angleInRadians);

                yield return new PointF((float)x, (float)y);

                angle += angleDelta;
            }
        }

        public IPositionedWord PositionNextWord(IPositionedWord word, SizeF size)
        {
            if (size.Width <= 0 || size.Height <= 0)
            {
                throw new ArgumentException(
                    "Width and height of size have to be > 0",
                    nameof(size));
            }

            var points = GetPoints();

            foreach (var point in points)
            {
                var rectangle = new RectangleF(point, size);

                if (!IsRectangleIntersectsWith(rectangle, rectangles))
                {
                    rectangles.Add(rectangle);

                    word.Position = rectangle;

                    return word;
                }
            }

            throw new InvalidOperationException(
                $"{nameof(GetPoints)} method didn't return new point by undefined reason.");
        }

        public static bool IsRectangleIntersectsWith(RectangleF rectangle, IEnumerable<RectangleF> rectangles)
        {
            return rectangles.Any(rectangle.IntersectsWith);
        }
    }
}