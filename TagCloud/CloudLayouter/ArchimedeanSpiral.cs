using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public class ArchimedeanSpiral
    {
        private readonly Random random = new Random(42);
        private double delta;
        private double parameter;
        private double angle;

        public ArchimedeanSpiral(SpiralSettings settings)
        {
            SetStartPoint(settings);
        }

        public void SetStartPoint(SpiralSettings settings)
        {
            angle = 0;
            parameter = settings.Parameter;
            delta = settings.Delta;
        }

        public IEnumerable<Point> GetNewPointLazy()
        {
            while (true)
            {
                angle += delta;
                var result = new Point(
                    (int) Math.Round(parameter * angle * Math.Sin(angle)) + random.Next(2, 10),
                    (int) Math.Round(0.5 * parameter * angle * Math.Cos(angle)) + random.Next(2, 10));
                yield return result;
            }
        }
    }
}