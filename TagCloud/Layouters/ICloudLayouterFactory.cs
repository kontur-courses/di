using System.Drawing;

namespace TagCloud.Layouters
{
    public interface ICloudLayouterFactory
    {
        ICloudLayouter Create(Point center);
    }
}
