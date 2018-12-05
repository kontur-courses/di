using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Layouters
{
    public class CircularCloudLayouter : AbstractCloudLayouter
    {
        private readonly List<RectangleF> usedRectangles;
        private float angle;
        private readonly double spiralLengthMultiplier;

        public CircularCloudLayouter(PointF center) : base(center)
        {
            angle = -1;
            spiralLengthMultiplier = 1e-2;
            usedRectangles = new List<RectangleF>();
        }

        public override RectangleF PutNextRectangle(SizeF rectangleSize)
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
            var dx = (float)(Math.Cos(angle) * angle * spiralLengthMultiplier);
            var dy = (float)(Math.Sin(angle) * angle * spiralLengthMultiplier);
            return new PointF(center.X + dx, center.Y + dy);
        }
    }
}