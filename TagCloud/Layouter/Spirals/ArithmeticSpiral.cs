using System;
using TagCloud.Interfaces;

namespace TagCloud.Layouter.Spirals
{
    public class ArithmeticSpiral : ISpiral
    {
        public Point Put(Point origin, double angle, double turnsInterval)
        {
            var x = origin.X + turnsInterval * angle * Math.Cos(angle);
            var y = origin.Y + turnsInterval * angle * Math.Sin(angle);

            return new Point(x, y);
        }
    }
}