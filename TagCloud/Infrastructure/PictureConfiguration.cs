using System.Drawing;

namespace TagCloud.Infrastructure
{
    public class PictureConfiguration
    {
        public Palette Palette { get; }
        public Size Size { get; set; }
        public Font Font { get; set; }

        public PictureConfiguration()
        {
            Palette = new Palette();
            Size = new Size(600, 600);
            Font = new Font(FontFamily.GenericMonospace, 10);
        }
    }
}
