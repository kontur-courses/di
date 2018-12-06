using System;
using System.Drawing;

namespace TagsCloudVisualization.PointGenerators
{
    public class Heart : IPointGenerator
    {
        private int nextPointCounter;

        public Point GetNextPoint()
        {
            var degree = new PointGeneratorSettings().DegreeStep * nextPointCounter;
            var factor = new PointGeneratorSettings().FactorStep * nextPointCounter;
            var x = (int)(1.3 * factor * Math.Cos(degree));
            var y = (int)(-factor * (Math.Sin(degree) + Math.Sqrt(Math.Abs(Math.Cos(degree)))));
            nextPointCounter++;

            return new Point(x, y);
        }
    }
}