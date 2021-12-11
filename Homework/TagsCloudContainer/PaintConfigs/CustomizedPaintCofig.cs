using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class CustomizedPaintCofig : IPaintConfig
    {
        public List<Brush> WordsColors { get; }
        public Font Font { get; }
        public Size ImageSize { get; }

        public CustomizedPaintCofig(IEnumerable<Brush> wordsColors, Font wordsFont, Size imageSize)
        {
            WordsColors = wordsColors.ToList();
            Font = wordsFont;
            ImageSize = imageSize;
        }

    }
}
