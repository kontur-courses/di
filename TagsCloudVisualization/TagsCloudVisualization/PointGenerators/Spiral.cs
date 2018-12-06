using System;
using System.Drawing;

namespace TagsCloudVisualization.PointGenerators
{
    public class Spiral : IPointGenerator
    {
        private int nextPointCounter;

        public Point GetNextPoint()
        {
            var degree = new PointGeneratorSettings().DegreeStep * nextPointCounter;
            var factor = new PointGeneratorSettings().FactorStep * nextPointCounter;
            var x = (int) (factor * Math.Sin(degree));
            var y = (int) (factor * Math.Cos(degree));
            nextPointCounter++;

            return new Point(x, y);
        }
    }
}