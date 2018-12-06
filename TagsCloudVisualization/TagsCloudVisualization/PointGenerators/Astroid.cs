using System;
using System.Drawing;

namespace TagsCloudVisualization.PointGenerators
{
    public class Astroid : IPointGenerator
    {
        private int nextPointCounter;

        public Point GetNextPoint()
        {
            var degree = new PointGeneratorSettings().DegreeStep * nextPointCounter;
            var factor = new PointGeneratorSettings().FactorStep * nextPointCounter;
            var x = (int) (factor * Math.Pow(Math.Cos(degree), 3));
            var y = (int) (factor * Math.Pow(Math.Sin(degree), 3));
            nextPointCounter++;

            return new Point(x, y);
        }
    }
}