using System;
using System.Drawing;

namespace TagsCloudContainer.TagCloudVisualization
{
    public class Spiral : IPlacingFunc
    {
        private readonly Func<double, Point> func;

        public Spiral() : this(0.1)
        {
        }

        private Spiral(double coefficient)
        {
            func = t => new Point(Convert.ToInt32(coefficient * t * Math.Sin(t)),
                Convert.ToInt32(coefficient * t * Math.Cos(t)));
        }

        public Point CalculatePoint(double param)
        {
            return func(param);
        }
    }
}