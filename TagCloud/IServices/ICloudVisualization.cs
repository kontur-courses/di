using System.Drawing;

namespace TagCloud
{
    public interface ICloudVisualization
    {
        Bitmap Image { get; }
        Bitmap ReDrawRectangles(ICloud cloud);
    }
}
