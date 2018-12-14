using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.LayoutGeneration
{
    public class SpiralGenerator: IEnumerable<Point>
    {
        private readonly Point center;
        private double angle;
        private double radius;
        private const double ShiftAngle = 0.3;
        private const double ShiftRadius = 0.003;

        public SpiralGenerator(Point center)
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("Coordinates of the center must be positive numbers");
            this.center = center;
        }

        public Point GetNextPositionOnSpiral()
        {
            using (var generator = GetEnumerator())
            {
                generator.MoveNext();
                return generator.Current;
            }
        }
        
        public IEnumerator<Point> GetEnumerator()
        {
            radius += ShiftRadius;
            angle += ShiftAngle;
            yield return new Point((int) (center.X + radius * Math.Cos(angle)),
                (int) (center.Y + radius * Math.Sin(angle)));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
