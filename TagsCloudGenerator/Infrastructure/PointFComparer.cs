using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.Infrastructure
{
    public class PointFComparer : IEqualityComparer<PointF>
    {
        public bool Equals(PointF first, PointF second)
        {
            return (Math.Abs(first.X - second.X) < double.Epsilon
                    && Math.Abs(first.Y - second.Y) < double.Epsilon);
        }

        public int GetHashCode(PointF point)
        {
            return point.GetHashCode();
        }
    }
}