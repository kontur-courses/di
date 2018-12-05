using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Visualizers
{
    public class ImageSettings
    {
        public int Width { get; }
        public int Height { get; }
        public ImageFormat Format { get; }
        public Font DefaultFont { get; }
        public int MinFontSize { get; }
        public int MaxFontSize { get; }

        public ImageSettings(int width, int height, ImageFormat format, Font defaultFont, int minFontSize, int maxFontSize)
        {
            Width = width;
            Height = height;
            Format = format;
            DefaultFont = defaultFont;
            MinFontSize = minFontSize;
            MaxFontSize = maxFontSize;
        }
    }
}