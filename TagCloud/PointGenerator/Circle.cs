using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.PointGenerator
{
    public class Circle : IPointGenerator
    {
        private static readonly Spiral Spiral = new Spiral(0.1f, 0.8, new(0, 0), new Cache());
        public IEnumerable<PointF> GetPoints(SizeF size)
        {
            return Spiral.GetPoints(size);
        }

        public PointF Center { get; } = Spiral.Center;
    }
}