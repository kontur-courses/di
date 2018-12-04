using System.Drawing;

namespace TagsCloudContainer
{
    public class Config
    {
        public Size ImageSize { get; }

        public Font Font { get; }

        public Color Color { get; }

        public Config(Size imageSize, Font font, Color color)
        {
            ImageSize = imageSize;
            Font = font;
            Color = color;
        }
    }
}