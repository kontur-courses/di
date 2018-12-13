using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Settings.Interfaces;

namespace TagCloud.Core.Layouters
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private PointF center;
        private readonly List<RectangleF> usedRectangles;
        private float angle;
        private readonly ILayoutingSettings settings;

        public CircularCloudLayouter(ILayoutingSettings settings)
        {
            this.settings = settings;
            center = new PointF(0, 0);
            angle = -1;
            usedRectangles = new List<RectangleF>();
        }

        public void RefreshWith(PointF newCenterPoint)
        {
            center = newCenterPoint;
            angle = -1;
            usedRectangles.Clear();
        }

        public RectangleF PutNextRectangle(SizeF rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Width and height should be positive numbers");

            RectangleF currentRectangle;
            do
            {
                var currentLocation = GetNextPoint();
                currentRectangle = new RectangleF(currentLocation, rectangleSize);
            } while (usedRectangles.Any(rect => rect.IntersectsWith(currentRectangle)));
            usedRectangles.Add(currentRectangle);
            return currentRectangle;
        }

        private PointF GetNextPoint()
        {
            angle++;
            var dx = (float)(Math.Cos(angle) * angle * settings.SpiralStepMultiplier);
            var dy = (float)(Math.Sin(angle) * angle * settings.SpiralStepMultiplier);
            return new PointF(center.X + dx, center.Y + dy);
        }
    }
}