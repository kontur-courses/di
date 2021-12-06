using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Layouters
{
    public class CircularCloudLayouter
    {
        public readonly HashSet<RectangleF> rectangles;
        private readonly Point center;
        private readonly IPointPlacer pointPlacer;

        public CircularCloudLayouter(Point center, IPointPlacer pointPlacer)
        {
            this.pointPlacer = pointPlacer;
            rectangles = new HashSet<RectangleF>();
        }

        public RectangleF PutNextRectangle(SizeF rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
            {
                throw new ArgumentException("Rectangle size should be positive floating point numbers");
            }

            var rect = GetNextRectanglePosition(rectangleSize);

            rectangles.Add(rect);

            return rect;
        }

        // ReSharper disable once UnusedMember.Global
        public IEnumerable<RectangleF> PutNextRectangles(IEnumerable<SizeF> rectanglesSizes)
            => rectanglesSizes.Select(PutNextRectangle);

        private RectangleF GetNextRectanglePosition(SizeF rectangleSize)
        {
            var rect = new RectangleF();
            do
            {
                rect = new RectangleF(
                    pointPlacer.CurrentPoint.X - rectangleSize.Width / 2,
                    pointPlacer.CurrentPoint.Y - rectangleSize.Height / 2,
                    rectangleSize.Height,
                    rectangleSize.Width);
                pointPlacer.GetNextPoint();
            } while (rect.IntersectsWithAnyOf(rectangles));

            return rect;
        }
    }
}