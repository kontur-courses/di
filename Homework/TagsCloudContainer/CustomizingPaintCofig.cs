using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class PaintConfig : IPaintConfig
    {
        public List<Pen> WordsColors { get; }
        public Font WordsFont { get; }
        public Size ImageSize { get; }

        public PaintConfig(IEnumerable<Pen> wordsColors, Font wordsFont, Size imageSize)
        {
            WordsColors = wordsColors.ToList();
            WordsFont = wordsFont;
            ImageSize = imageSize;
        }

    }
}
