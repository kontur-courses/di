using System;
using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class ArchimedeanSpiral : ISpiral
    {
        private readonly double angleDelta;

        private readonly Point center;
        private readonly double distanceBetweenLoops;
        private double angle;

        public ArchimedeanSpiral(Point center, double distanceBetweenLoops, double angleDelta)
        {
            angle = 0;
            this.center = center;
            this.angleDelta = angleDelta;
            this.distanceBetweenLoops = distanceBetweenLoops;

            ValidateSpiralParameters();
        }

        public Point GetNextPoint()
        {
            var x = center.X + (int) (distanceBetweenLoops * angle * Math.Cos(angle));
            var y = center.Y + (int) (distanceBetweenLoops * angle * Math.Sin(angle));
            angle += angleDelta;

            return new Point(x, y);
        }

        private void ValidateSpiralParameters()
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("center coordinates should not be negative numbers");

            if (angleDelta <= 0)
                throw new ArgumentException("angleDelta should not be negative or zero");

            if (distanceBetweenLoops <= 0)
                throw new ArgumentException("distanceBetweenLoops should not be negative or zero");
        }
    }
}