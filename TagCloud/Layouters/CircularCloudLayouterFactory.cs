using System.Drawing;

namespace TagCloud.Layouters
{
    public class CircularCloudLayouterFactory : ICloudLayouterFactory
    {
        public ICloudLayouter Create(Point center)
        {
            return new CircularCloudLayouter(center);
        }
    }
}
