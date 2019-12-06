using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloud.Visualization.Tag;

namespace TagsCloud.Visualization.SizeDefiner
{
    public class FrequencySizeDefiner : ISizeDefiner
    {
        private readonly string _fontName;
        private readonly int _maxFontSize;
        private readonly int _minFontSize;

        public FrequencySizeDefiner(string fontName, int minFontSize, int maxFontSize)
        {
            this._fontName = fontName;
            this._maxFontSize = maxFontSize;
            this._minFontSize = minFontSize;
        }

        public TagSize GetTagSize(string word, int frequency, int minFrequency, int maxFrequency)
        {
            var fontSize = GetFontSize(frequency, minFrequency, maxFrequency);
            var size = TextRenderer.MeasureText(word, new Font(_fontName, fontSize));
            return new TagSize(size, fontSize);
        }

        private int GetFontSize(int frequency, int minFrequency, int maxFrequency)
        {
            var intervalSize = (maxFrequency - minFrequency);
            intervalSize = (intervalSize == 0) ? maxFrequency : intervalSize;
            return _maxFontSize - (_maxFontSize - _minFontSize) * (maxFrequency - frequency) / intervalSize;
        }
    }
}