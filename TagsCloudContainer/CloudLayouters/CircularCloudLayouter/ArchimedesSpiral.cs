using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CloudLayouters.CircularCloudLayouter
{
    public class ArchimedesSpiral
    {
        public Point Center { get;  }
        public double Step { get; }
        private int theta;

        public ArchimedesSpiral(Point center, double step)
        {
            Center = center;
            Step = step;
        }
        
        public IEnumerable<Point> GetSpiralPoints()
        {
            while (true)
            {
                var radius = Step * theta;
                var x = (int)(radius * Math.Cos(theta));
                var y = (int)(radius * Math.Sin(theta));
                theta++;
                x += Center.X;
                y += Center.Y;
                yield return new Point(x, y);
            }
        }
    }
}