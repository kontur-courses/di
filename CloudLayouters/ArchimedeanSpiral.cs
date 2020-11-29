using System;
using System.Drawing;

namespace CloudLayouters
{
    public class ArchimedeanSpiral
    {
        private readonly double anglesDelta = 0.005;
        private readonly double Distance = 0.5;
        private double angle;
        private Point? previous;

        public ArchimedeanSpiral(Point center)
        {
            Center = center;
        }

        public ArchimedeanSpiral(Point center, double distance, double anglesDelta)
        {
            Center = center;
            Distance = distance;
            this.anglesDelta = anglesDelta;
        }

        public Point Center { get; }

        public Point GetNextPoint()
        {
            Point result;
            do
            {
                var radius = Distance * angle;
                var x = radius * Math.Cos(angle);
                var y = radius * Math.Sin(angle);
                angle += anglesDelta;
                result = new Point((int) Math.Round(x) + Center.X, (int) Math.Round(y) + Center.Y);
            } while (result == previous);

            previous = result;
            return result;
        }
    }
}