using System;
using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class ArchimedeanSpiral
    {
        public ArchimedeanSpiral(Point center, double distanceBetweenLoops, double angleDelta)
        {
            Angle = 0;
            Center = center;
            AngleDelta = angleDelta;
            DistanceBetweenLoops = distanceBetweenLoops;

            ValidateSpiralParameters();
        }

        private Point Center { get; }
        private double DistanceBetweenLoops { get; }
        private double Angle { get; set; }
        private double AngleDelta { get; }

        private void ValidateSpiralParameters()
        {
            if (Center.X < 0 || Center.Y < 0)
                throw new ArgumentException("center coordinates should not be negative numbers");

            if (AngleDelta <= 0) throw new ArgumentException("angleDelta should not be negative or zero");

            if (DistanceBetweenLoops <= 0)
                throw new ArgumentException("distanceBetweenLoops should not be negative or zero");
        }

        public Point GetNextPoint()
        {
            var x = Center.X + (int) (DistanceBetweenLoops * Angle * Math.Cos(Angle));
            var y = Center.Y + (int) (DistanceBetweenLoops * Angle * Math.Sin(Angle));
            Angle += AngleDelta;

            return new Point(x, y);
            ;
        }
    }
}