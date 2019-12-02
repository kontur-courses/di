using System.Drawing;

namespace TagCloud
{
    public class CloudVisualization : ICloudVisualization
    {
        public Bitmap Image { get; } = new Bitmap(Settings.Width, Settings.Height);

        public Bitmap ReDrawRectangles(ICloud cloud)
        {
            return Image;
        }
    }
}
