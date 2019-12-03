using System.Drawing;

namespace TagCloud
{
    public class CloudVisualization : ICloudVisualization
    {
        public Bitmap Image { get; }
        public CloudVisualization(ICloud cloud)
        {
            Image = new Bitmap(cloud.Data.Width,cloud.Data.Height);
        }
        public Bitmap ReDrawRectangles(ICloud cloud)
        {
            return Image;
        }
    }
}
