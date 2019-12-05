using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudGenerator.Visualizer
{
    public class ImageSettings
    {
        public int Width { get; }
        public int Height { get; }
        public Color BackgroundColor { get; }
        public IReadOnlyList<Color> Colors { get; }
        public ImageFormat ImageFormat { get; }
        public Font Font { get; }

        public ImageSettings(int width, int height, Color backgroundColor, IReadOnlyList<Color> colors, ImageFormat imageFormat, Font font)
        {
            Width = width;
            Height = height;
            BackgroundColor = backgroundColor;
            Colors = colors;
            ImageFormat = imageFormat;
            Font = font;
        }
    }
}