using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    internal class SpiralPointProvider : IProvider<Point>
    {
        private readonly Point center;
        private double phi;
        private int x;
        private int y;

        public SpiralPointProvider(Point center)
        {
            this.center = center;
        }

        public Point GetNext()
        {
            phi += 0.1;
            x = center.X + (int)Math.Floor(0.001 * phi * Math.Cos(phi));
            y = center.Y + (int)Math.Floor(0.001 * phi * Math.Sin(phi));
            return new Point(x, y);
        }
    }
}
