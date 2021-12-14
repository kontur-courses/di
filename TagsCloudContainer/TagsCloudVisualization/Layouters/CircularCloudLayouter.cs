using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.PointPlacers;

namespace TagsCloudVisualization.Layouters
{
    public class CircularCloudLayouter : ILayouter
    {
        private readonly HashSet<RectangleF> rectangles;
        private readonly IPointPlacer pointPlacer;

        public IEnumerable<RectangleF> PlacedRectangles => rectangles;
        
        public CircularCloudLayouter(IPointPlacer pointPlacer)
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
        
        public IEnumerable<RectangleF> PutNextRectangles(IEnumerable<SizeF> rectanglesSizes)
            => rectanglesSizes.Select(PutNextRectangle);

        private RectangleF GetNextRectanglePosition(SizeF rectangleSize)
        {
            RectangleF rect;
            do
            {
                rect = new RectangleF(
                    pointPlacer.CurrentPoint.X - rectangleSize.Width / 2,
                    pointPlacer.CurrentPoint.Y - rectangleSize.Height / 2,
                    rectangleSize.Width,
                    rectangleSize.Height);
                pointPlacer.GetNextPoint();
            } while (rect.IntersectsWithAnyOf(rectangles));

            return rect;
        }
    }
}