using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class ArchimedesSpiral : IEnumerable<Point>
    {
        private readonly Func<ArchimedesSpiralEnumerator> spiralEnumerator;
        
        public ArchimedesSpiral(Point center, float radius = 0.5f, float increment = 0.5f, float angle = 0)
        {
            if (Math.Abs(radius) < float.Epsilon)
                throw new ArgumentException("Spiral radius absolute value can't be less then float.Epsilon");
            if (Math.Abs(increment) < float.Epsilon)
                throw new ArgumentException("Spiral increment absolute value can't be less then float.Epsilon");

            spiralEnumerator = () =>  new ArchimedesSpiralEnumerator(center, radius, increment, angle);
        }

        public IEnumerator<Point> GetEnumerator() => spiralEnumerator.Invoke();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class ArchimedesSpiralEnumerator : IEnumerator<Point>
        {
            private readonly Point center;
            private readonly float increment;
            private readonly float radius;
            private readonly float startAngle;
            private float angle;

            internal ArchimedesSpiralEnumerator(Point center, float radius, float increment, float angle)
            {
                this.center = center;
                this.radius = radius;
                this.increment = increment;
                this.angle = angle;
                startAngle = angle;
                Current = center;
            }

            public Point Current { get; private set; }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                try
                {
                    checked
                    {
                        angle += increment;
                        var x = center.X + (int) Math.Round(Math.Cos(angle) * (angle * radius));
                        var y = center.Y + (int) Math.Round(Math.Sin(angle) * (angle * radius));
                        Current = new Point(x, y);
                    }
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(e);
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                Current = center;
                angle = startAngle;
            }

            public void Dispose()
            {
            }
        }
    }
}