using System;
using System.Drawing;

namespace TagCloud.Utils
{
    internal static class CoordinatesConverter
    {
        public static Point ToCartesian(double rho, double phi)
        {
            return new Point((int) Math.Round(rho * Math.Cos(phi)), (int) Math.Round(rho * Math.Sin(phi)));
        }

        public static (double rho, double phi) ToPolar(Point point)
        {
            var rho = Math.Sqrt(point.X * point.X + point.Y * point.Y);
            var phi = Math.Atan2(point.Y, point.X);
            return (rho, phi);
        }
    }
}