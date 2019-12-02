using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public class RectangleSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Size RectangleSize => new Size(Width, Height);
    }
}