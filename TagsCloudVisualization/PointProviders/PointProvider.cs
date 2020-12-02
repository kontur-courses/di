using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class PointProvider : IPointProvider
    {
        private double angle, radius;
        private const double SpiralParameter = 0.01;

        public PointProvider()
        {

        }

        public Point GetPoint(IConfig config)
        {
            var x = (int)Math.Round(radius * Math.Cos(angle));
            var y = (int)Math.Round(radius * Math.Sin(angle));

            radius += SpiralParameter;
            angle += Math.PI / 180;

            return new Point(config.Center.X - x, config.Center.Y - y);
        }

    }
}
