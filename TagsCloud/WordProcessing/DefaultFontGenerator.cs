using TagsCloud.Interfaces;
using System.Drawing;

namespace TagsCloud.WordProcessing
{
    public class DefaultFontGenerator : IFontSettingsGenerator
    {
        private readonly int maxFontSize;
        private readonly int minFontSize;
        private readonly string fontName;

        public DefaultFontGenerator(string fontName, int maxFontSize=128, int minFontSize=40)
        {
            this.fontName = fontName;
            this.maxFontSize = maxFontSize;
            this.minFontSize = minFontSize;
        }

        public Font GetFontSizeForCurrentWord((string word, int frequency) wordFrequency, int positionByFrequency, int countWords)
        {
            float fontSize =  maxFontSize * ((float)(countWords - positionByFrequency + 1) / countWords);
            fontSize = fontSize > minFontSize ? fontSize : minFontSize;
            return new Font(fontName, fontSize);
        }
    }
}
