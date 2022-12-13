using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.WordsColoringAlgorithms
{
    public class GradientDependsOnSizePainter : IWordsPainter
    {
        public Color[] GetColorsSequence(Dictionary<string, int> frequencyDictionary, string startColor)
        {
            var maxWordCount = frequencyDictionary.Values.Max();
            var color = Color.FromName(startColor);
            if (!color.IsKnownColor)
                throw new ArgumentException("Unknown brush color");
            var resultA = color.A / maxWordCount;
            var colors = new Color[frequencyDictionary.Count];
            var counter = 0;
            foreach (var wordCount in frequencyDictionary.Values)
            {
                colors[counter] = Color.FromArgb(resultA * wordCount, color.R, color.G, color.B);
                counter++;
            }

            return colors;
        }
    }
}