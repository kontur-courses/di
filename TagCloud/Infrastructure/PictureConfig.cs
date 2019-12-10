using System.Drawing;
using TagCloud.Algorithm.SpiralBasedLayouter;

namespace TagCloud.Infrastructure
{
    public class PictureConfig
    {
        public Palette Palette { get; set; }
        public Size Size { get; set; }
        public FontFamily FontFamily { get; set; }
        public Point Center => new Point(Size.Width / 2, Size.Height / 2);
        public LayouterParameters Parameters { get; set; }
        public static Size MinSize = new Size(300, 200);

        public PictureConfig()
        {
            Palette = new Palette();
            FontFamily = new FontFamily("Comic Sans MS");
            Parameters = new LayouterParameters();
        }
    }
}
