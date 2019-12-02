using System;
using System.Drawing;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.PointsSearchers
{
    public class PointsSearcherOnSpiral : IPointsSearcher
    {
        private const double maxAngle = 2 * Math.PI;

        private int radius = 0;
        private double angle = 0;
        private double step = 1;

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
    }
}