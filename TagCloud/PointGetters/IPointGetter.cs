using System.Drawing;

namespace TagCloud.PointGetters
{
    internal interface IPointGetter
    {
        public Point Center { get; }
        public Point GetNextPoint();
    }
}
