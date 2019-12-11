using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class ArchimedeanSpiralLayouter : ILayouter
    {
        private readonly ImageSettings imageSettings;
        private readonly LayouterSettings layouterSettings;
        private List<RectangleF> rectangles;
        private double angle;

        public ArchimedeanSpiralLayouter(LayouterSettings layouterSettings, ImageSettings imageSettings)
        {
            this.layouterSettings = layouterSettings;
            this.imageSettings = imageSettings;
            rectangles = new List<RectangleF>();
            angle = 0;
        }

        public void Reset()
        {
            rectangles = new List<RectangleF>();
            angle = 0;
        }

        public RectangleF PutNextRectangle(SizeF rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Invalid size");
            var center = imageSettings.CloudCenter;
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
            var x = (float)(Math.Cos(angle) * (angle * layouterSettings.Radius));
            var y = (float)(Math.Sin(angle) * (angle * layouterSettings.Radius));
            angle += layouterSettings.Step;
            return new PointF(imageSettings.CloudCenter.X + x, imageSettings.CloudCenter.Y + y);
        }
    }
}
