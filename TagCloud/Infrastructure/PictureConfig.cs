using System.Drawing;

namespace TagCloud.Infrastructure
{
    public class PictureConfig
    {
        public Palette Palette { get; }
        public Size Size { get; set; }
        public FontFamily FontFamily { get; set; }
        public Point Center => new Point(Size.Width / 2, Size.Height / 2);

        public PictureConfig()
        {
            Palette = new Palette();
            Size = new Size(2700, 2000);
            FontFamily = new FontFamily("Comic Sans MS");
        }
    }
}
