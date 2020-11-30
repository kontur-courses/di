using System.Drawing;

namespace TagsCloud
{
    public class ImageSettings
    {
        public ImageSettings(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public Palette Palette { get; } = new Palette();
        public Font Font { get; set; } = new Font(FontFamily.GenericSerif, 14);
        public int Width { get; set; }
        public int Height { get; set; }
    }
}