using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.WordsColoringAlgorithms
{
    public class GradientWordsPainter : IWordsPainter
    {
        public Color[] GetColorsSequence(Dictionary<string, int> frequencyDictionary, string startColor)
        {
            var color = Color.FromName(startColor);
            if (!color.IsKnownColor)
                throw new ArgumentException("Unknown brush color");
            var counter = 0;
            var colors = new Color[frequencyDictionary.Count];
            while (counter < frequencyDictionary.Count)
            {
                var resultA = color.A - 10 > 0 ? color.A - 10 : 0;
                color = Color.FromArgb(resultA, color.R, color.G, color.B);
                colors[counter] = color;
                counter++;
            }

            return colors;
        }
    }
}