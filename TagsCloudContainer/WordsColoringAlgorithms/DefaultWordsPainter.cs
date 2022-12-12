using System;
using System.Drawing;

namespace TagsCloudContainer.WordsColoringAlgorithms
{
    public class DefaultWordPainter : IWordStainer
    {
        public Color[] GetColorsSequence(int wordsCount, string startColor)
        {
            var color = Color.FromName(startColor);
            if (!color.IsKnownColor)
                throw new ArgumentException("Unknown brush color");
            var colors = new Color[wordsCount];
            for (var i = 0; i < wordsCount; i++)
                colors[i] = color;
            return colors;
        }
    }
}