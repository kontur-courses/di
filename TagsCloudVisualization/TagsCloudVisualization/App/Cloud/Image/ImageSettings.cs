using System.Drawing;

namespace TagsCloudVisualization
{
    public class ImageSettings : IImageSettings
    {
        public Point Center { get; set; } = new Point(500, 500);
        public Size Size { get; set; } = new Size(1000, 1000);
    }
}
