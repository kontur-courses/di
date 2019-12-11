using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.CloudLayouter.CircularLayouter
{
    public class ArchimedeanSpiral
    {
        private readonly Random random = new Random(42);
        private readonly SpiralSettings settings;
        private double angleChange;
        private double densityParameter;
        private double angle;

        public ArchimedeanSpiral(SpiralSettings settings)
        {
            this.settings = settings;
            SetNewStartPoint();
        }

        public void SetNewStartPoint()
        {
            angle = 0;
            densityParameter = settings.DensityParameter;
            angleChange = settings.AngleChange;
        }

        public IEnumerable<Point> GetNewPointLazy()
        {
            while (true)
            {
                angle += angleChange;
                var result = new Point(
                    (int) Math.Round(densityParameter * angle * Math.Sin(angle)) + random.Next(2, 10),
                    (int) Math.Round(0.5 * densityParameter * angle * Math.Cos(angle)) + random.Next(2, 10));
                yield return result;
            }
        }
    }
}