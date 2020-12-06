using System;
using System.Drawing;

namespace CloudLayouters
{
    public class ArchimedeanSpiral
    {
        private readonly double anglesDelta;
        private readonly double distance;
        private double angle;
        private Point? previous;


        public ArchimedeanSpiral(Point center, double distance = 0.5, double anglesDelta = 0.005)
        {
            Center = center;
            this.distance = distance;
            this.anglesDelta = anglesDelta;
        }

        public Point Center { get; }

        public Point GetNextPoint()
        {
            Point result;
            do
            {
                var radius = distance * angle;
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