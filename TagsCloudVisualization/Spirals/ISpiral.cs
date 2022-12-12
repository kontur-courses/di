using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Spirals
{
    public interface ISpiral
    {
        public Point Center { get; }
        public List<Point> Points { get; }
        public List<Point> GetPoints(int count);
    }
}
