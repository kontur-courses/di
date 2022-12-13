using System.Drawing;

namespace TagCloud.PointGenerator
{
    public class EllipsePointGenerator : ArchimedeanSpiral
    {
        public EllipsePointGenerator(Point centralPoint)
            : base(centralPoint, 0.5, 1) { }

        public EllipsePointGenerator()
            : this(new Point(0, 0)) { }
    }
}
