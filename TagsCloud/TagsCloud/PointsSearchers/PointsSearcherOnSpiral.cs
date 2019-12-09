using System;
using System.Drawing;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.PointsSearchers
{
    [Factorial("PointsSearcherOnSpiral")]
    public class PointsSearcherOnSpiral : IPointsSearcher
    {
        private const double maxAngle = 2 * Math.PI;

        private int radius;
        private double angle;
        private double step;

        public PointsSearcherOnSpiral() => Reset();

        public PointF GetNextPoint()
        {
            if ((angle += step) >= maxAngle)
            {
                radius++;
                angle = 0;
                step = Math.PI / Math.Pow(radius + 1, 0.7);
            }
            return new PointF(
                x: (float)(radius * Math.Cos(angle)),
                y: (float)(radius * Math.Sin(angle)));
        }

        public void Reset()
        {
            radius = 0;
            angle = 0;
            step = 1;
        }
    }
}