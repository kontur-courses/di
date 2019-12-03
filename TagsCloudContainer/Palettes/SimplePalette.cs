using System.Drawing;

namespace TagsCloudContainer.Palettes
{
    class SimplePalette : IPalette
    {
        public Font Font { get; }
        public Brush Brush { get; }

        public SimplePalette(Font font, Brush brush)
        {
            Font = font;
            Brush = brush;
        }
    }
}
