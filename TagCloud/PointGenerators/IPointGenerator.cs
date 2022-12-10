using System.Drawing;

namespace TagCloud.PointGenerators
{
    public interface IPointGenerator
    {
        public delegate IPointGenerator Factory();
        public Point GetNextPoint();
        public Point GetCenterPoint();
    }
}
