using TagsCloud.Interfaces;
using System.Drawing;
using System;

namespace TagsCloud.WordProcessing
{
    public class DefaultFontGenerator : IFontSettingsGenerator
    {
        private readonly int maxFontSize;
        private readonly int minFontSize;
        private readonly string fontName;

        public DefaultFontGenerator(TagCloudSettings fontName, int maxFontSize=128, int minFontSize=40)
        {
            this.fontName = fontName.FontName;
            this.maxFontSize = maxFontSize;
            this.minFontSize = minFontSize;
        }

        public FontSettings GetFontSizeForCurrentWord((string word, int frequency) wordFrequency, int positionByFrequency, int countWords)
        {
            float fontSize =  maxFontSize * ((float)(countWords - positionByFrequency + 1) / countWords);
            fontSize = Math.Max(fontSize, minFontSize);
            return new FontSettings(fontName, fontSize);
        }
    }
}
