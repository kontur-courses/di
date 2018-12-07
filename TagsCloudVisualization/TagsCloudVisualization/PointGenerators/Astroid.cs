using System;
using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.PointGenerators
{
    public class Astroid : IPointGenerator
    {
        private int nextPointCounter;

        public Point GetNextPoint(IPointGeneratorSettings settings)
        {
            var degree = settings.DegreeStep * nextPointCounter;
            var factor = settings.FactorStep * nextPointCounter;
            var x = (int) (factor * Math.Pow(Math.Cos(degree), 3));
            var y = (int) (factor * Math.Pow(Math.Sin(degree), 3));
            nextPointCounter++;

            return new Point(x, y);
        }
    }
}