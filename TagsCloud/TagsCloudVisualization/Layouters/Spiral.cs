using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Layouters
{
    public class Spiral : IEnumerable<PointF>
    {
        private readonly Func<SpiralEnumerator> spiralEnumerator;

        public Spiral(PointF center = default, float radius = 0.5f, float increment = 0.5f, float angle = 0)
        {
            if (Math.Abs(radius) < float.Epsilon)
                throw new ArgumentException("Spiral radius absolute value can't be less then float.Epsilon");
            if (Math.Abs(increment) < float.Epsilon)
                throw new ArgumentException("Spiral increment absolute value can't be less then float.Epsilon");

            spiralEnumerator = () => new SpiralEnumerator(center, radius, increment, angle);
        }

        public IEnumerator<PointF> GetEnumerator() => spiralEnumerator.Invoke();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class SpiralEnumerator : IEnumerator<PointF>
        {
            private readonly PointF center;
            private readonly float increment;
            private readonly float radius;
            private readonly float startAngle;
            private float angle;

            internal SpiralEnumerator(PointF center, float radius, float increment, float angle)
            {
                this.center = center;
                this.radius = radius;
                this.increment = increment;
                this.angle = angle;
                startAngle = angle;
                Current = center;
            }

            public PointF Current { get; private set; }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                angle += increment;
                var x = center.X + (float) (Math.Cos(angle) * (angle * radius));
                var y = center.Y + (float) (Math.Sin(angle) * (angle * radius));
                Current = new PointF(x, y);

                if (float.IsInfinity(x) || float.IsInfinity(y))
                {
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