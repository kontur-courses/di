using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Distribution
{
    public class ArchimedeanSpiral : IDistribution
    {
        private const double DegreeInRadians = Math.PI / 180;
        private const double FullRotation = 2 * Math.PI;

        public ArchimedeanSpiral(Point center, double stepOnX = 2, double stepOnY = 1,
            double angleStep = DegreeInRadians)
        {
            Center = center;
            StepOnX = stepOnX;
            StepOnY = stepOnY;
            AngleStep = angleStep;
            AngleInRadians = 0;
        }

        private Point Center { get; }
        private double StepOnX { get; }
        private double StepOnY { get; }
        private double AngleStep { get; }
        private double AngleInRadians { get; set; }

        private double OffsetFromCenterOnX
            => StepOnX / FullRotation * AngleInRadians * Math.Sin(AngleInRadians);

        private double OffsetFromCenterOnY
            => StepOnY / FullRotation * AngleInRadians * Math.Cos(AngleInRadians);

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
    }
}