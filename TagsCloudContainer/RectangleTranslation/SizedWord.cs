using System.Drawing;
using TagsCloudContainer.Visualization;

namespace TagsCloudContainer.RectangleTranslation
{
    public class SizedWord
    {
        public readonly string Word;
        public readonly int FontSize;
        public readonly SizeF WordSize;

        public SizedWord(string word, int fontSize)
        {
            Word = word;
            FontSize = fontSize;

            WordSize = StringMeasurer.MeasureString(word, fontSize);
        }
    }
}