using System;
using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud.ColorSchemes
{
    class RedGreenBlueScheme : IColorScheme
    {
        public Color GetColorForCurrentWord((string word, int frequency) wordFrequency, int positionByFrequency, int countWords)
        {
            var alpha = (int)(255 * ((float)(countWords - positionByFrequency + 1) / countWords));
            var position = (float)positionByFrequency / countWords;
            var color = position < 0.1 ? Color.Red : position < 0.40 ? Color.Green : Color.Blue;
            alpha = Math.Min(255, alpha);
            return Color.FromArgb(alpha, color);
        }
    }
}
