using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class PointProvider : IPointProvider
    {
        private double angle, radius;
        private const double SpiralParameter = 0.01;
        private readonly IConfig config;

        public PointProvider(IConfig config)
        {
            if (config.Center.X < 0 || config.Center.Y < 0)
                throw new ArgumentException("X or Y of center was negative");

            this.config = config;
        }

        public Point GetPoint()
        {
            var x = (int)Math.Round(radius * Math.Cos(angle));
            var y = (int)Math.Round(radius * Math.Sin(angle));

            radius += SpiralParameter;
            angle += Math.PI / 180;

            return new Point(config.Center.X - x, config.Center.Y - y);
        }

    }
}
