using System;
using System.Drawing;

namespace TagsCloudContainer.Layout
{
    public class Spiral
    {
        private const double ThetaDelta = 0.01;

        public readonly Point Center;
        public double Angle { get; private set; }
        public double Radius => Angle;

        public Spiral(Point center)
        {
            Center = center;
        }

        public PointF GetNextLocation()
        {
            var x = (float) (Radius * Math.Cos(Angle) + Center.X);
            var y = (float) (Radius * Math.Sin(Angle) + Center.Y);

            Angle += ThetaDelta;

            return new PointF(x, y);
        }
    }
}