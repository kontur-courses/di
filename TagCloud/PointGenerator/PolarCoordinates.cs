using System;
using System.Drawing;

namespace TagCloud.PointGenerator
{
    public readonly struct PolarCoordinates
    {
        public float R { get; }

        public float Angle { get; }

        public PolarCoordinates(float r, float angle)
        {
            R = r;
            Angle = angle;
        }

        public PointF ToCartesian()
        {
            var x = (float)(R * Math.Cos(Angle));
            var y = (float)(R * Math.Sin(Angle));
            return new PointF(x, y);
        }
    }
}