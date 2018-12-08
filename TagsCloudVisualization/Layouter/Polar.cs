using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public abstract class Polar
    {
        public double Radius { get; protected set; }
        public double Angle { get; protected set; }

        public Point ToCartesian() => new Point((int)(Radius * Math.Cos(Angle)), (int)(Radius * Math.Sin(Angle)));
        
    }
}