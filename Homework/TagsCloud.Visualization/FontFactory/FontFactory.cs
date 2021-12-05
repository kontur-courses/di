using System.Drawing;
using TagsCloud.Visualization.Models;

namespace TagsCloud.Visualization.FontFactory
{
    public class FontFactory : IFontFactory
    {
        private const int MaxFontSize = 2000;
        private const string FamilyName = "Times new roman";
        private const FontStyle FontStyle = System.Drawing.FontStyle.Regular;

        public FontDecorator GetFont(Word word, int minCount, int maxCount)
        {
            var fontSize = word.Count <= minCount
                ? 1
                : MaxFontSize * (word.Count - minCount) / (maxCount - minCount);
            return new FontDecorator(FamilyName, fontSize, FontStyle);
        }
    }
}