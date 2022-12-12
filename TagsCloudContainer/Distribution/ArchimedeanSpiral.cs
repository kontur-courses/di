using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class ArchimedeanSpiral : IDistribution
    {
        const double DegreeInRadians = Math.PI / 180;
        const double FullRotation = 2 * Math.PI;
        
        public Point Center { get; }
        public double StepOnX { get; }
        public double StepOnY { get; }
        public double AngleStep { get; }
        public double AngleInRadians { get; private set; }

        public ArchimedeanSpiral(Point center,  double stepOnX = 2, double stepOnY = 1,
            double angleStep = DegreeInRadians)
        {
            Center = center;
            StepOnX = stepOnX;
            StepOnY = stepOnY;
            AngleStep = angleStep;
            AngleInRadians = 0;
        }

        public IEnumerable<Point> GetPoints()
        {
            while (true)
            {
                var x = Center.X + OffsetFromCenterOnX;
                var y = Center.Y + OffsetFromCenterOnY;
                yield return new Point((int)x, (int)y);

                AngleInRadians += AngleStep;
            }
        }

        private double OffsetFromCenterOnX 
            => (StepOnX / FullRotation) * AngleInRadians * Math.Sin(AngleInRadians);

        private double OffsetFromCenterOnY
            => (StepOnY / FullRotation) * AngleInRadians * Math.Cos(AngleInRadians);
    }
}