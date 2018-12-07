using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class Config
    {
        public Size ImageSize { get; }

        public Font Font { get; }

        public Color Color { get; }

        public IEnumerable<string> BoringWords { get; set; } = Enumerable.Empty<string>();

        public Config(Size imageSize, Font font, Color color)
        {
            ImageSize = imageSize;
            Font = font;
            Color = color;
        }
    }
}