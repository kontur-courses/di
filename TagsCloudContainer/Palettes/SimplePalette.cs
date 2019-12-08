using System.Drawing;
using TagsCloudContainer.PaintersWords;

namespace TagsCloudContainer.Palettes
{
    class SimplePalette : IPalette
    {
        public Font Font { get; }
        public IPainterWords PainterWords { get; }

        public SimplePalette(Font font, IPainterWords painterWords)
        {
            Font = font;
            PainterWords = painterWords;
        }
    }
}
