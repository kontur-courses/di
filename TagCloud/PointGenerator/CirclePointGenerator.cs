using System.Drawing;

namespace TagCloud.PointGenerator
{
    public class CirclePointGenerator : ArchimedeanSpiral
    {
        public CirclePointGenerator(Point centralPoint) 
            : base(centralPoint, 1, 1) { }
    }
}
