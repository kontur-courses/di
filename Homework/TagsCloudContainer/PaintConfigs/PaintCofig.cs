using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class PaintCofig : IPaintConfig
    {
        public List<Brush> WordsColors { get; }
        public string FontName { get; }
        public int FontSize { get; }
        public Size ImageSize { get; }

        public PaintCofig(IEnumerable<Brush> wordsColors, 
            string fontName, int fontSize, Size imageSize)
        {
            WordsColors = wordsColors.ToList();
            FontName = fontName;
            FontSize = fontSize;
            ImageSize = imageSize;
        }

        public Brush GetRandomBrush()
        {
            var rnd = new Random();
            return WordsColors[rnd.Next(WordsColors.Count)];
        }

    }
}
