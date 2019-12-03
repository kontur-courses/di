using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class ArchimedeanSpiralLayouter : ILayouter
    {
        private readonly List<RectangleF> rectangles;
        private readonly PointF cloudCenter;
        private readonly float radius;
        private readonly double step;
        private double angle = 0;

        public ArchimedeanSpiralLayouter(LayouterSettings layouterSettings, ImageSettings imageSettings)
        {
            cloudCenter = imageSettings.CloudCenter;
            radius = layouterSettings.Radius;
            step = layouterSettings.Step;
            rectangles = new List<RectangleF>();
        }

        public RectangleF PutNextRectangle(SizeF rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Invalid size");
            var center = cloudCenter;
            var corner = GetRectangleCorner(center, rectangleSize);
            var nextRectangle = new RectangleF(corner, rectangleSize);
            while (IsIntersectingOrOutOfBorders(nextRectangle))
            {
                center = GetNextRectangleCenter();
                corner = GetRectangleCorner(center, rectangleSize);
                nextRectangle.Location = corner;
            }
            rectangles.Add(nextRectangle);
            return new RectangleF(nextRectangle.Location, rectangleSize);
        }

        private bool IsIntersectingOrOutOfBorders(RectangleF nextRectangle) =>
            nextRectangle.X < 0 || nextRectangle.Y < 0 ||
            rectangles.Any(rectangle => rectangle.IntersectsWith(nextRectangle));

        private PointF GetRectangleCorner(PointF center, SizeF size) =>
            new PointF(center.X - size.Width / 2, center.Y - size.Height / 2);

        private PointF GetNextRectangleCenter()
        {
            var t = Math.PI * angle;
            var x = (float)(Math.Cos(angle) * (angle * radius));
            var y = (float)(Math.Sin(angle) * (angle * radius));
            angle += step;
            return new PointF(cloudCenter.X + x, cloudCenter.Y + y);
        }
    }
}
