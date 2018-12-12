using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Extensions;

namespace TagCloud.PointsSequence
{
    public class Spiral : PointsSequence
    {
        private float alpha;
        private float stepLength;

        public Spiral() : base(Point.Empty)
        {
            SetStepLength(1);
        }

        public override void Reset()
        {
            alpha = 0;
        }

        public void SetStepLength(float newStepLength)
        {
            if (newStepLength <= 0)
                throw new ArgumentException("step can't be negative or zero");
            stepLength = newStepLength;
        }

        protected override IEnumerable<Point> Sequence()
        {
            while (true)
            {
                var ro = stepLength / (Math.PI * 2);
                yield return new Point
                {
                    X = (int)(ro * alpha * Math.Cos(alpha.ToRadians())) + center.X,
                    Y = (int)(ro * alpha * Math.Sin(alpha.ToRadians())) + center.Y
                };
                alpha++;
            }
        }
    }
}