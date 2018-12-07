using System;
using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.PointGenerators
{
    public class Spiral : IPointGenerator
    {
        private int nextPointCounter;

        public Point GetNextPoint(IPointGeneratorSettings settings)
        {
            var degree = settings.DegreeStep * nextPointCounter;
            var factor = settings.FactorStep * nextPointCounter;
            var x = (int) (factor * Math.Sin(degree));
            var y = (int) (factor * Math.Cos(degree));
            nextPointCounter++;

            return new Point(x, y);
        }
    }
}