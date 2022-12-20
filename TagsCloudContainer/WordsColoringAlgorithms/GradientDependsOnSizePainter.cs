using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.WordsColoringAlgorithms
{
    public class GradientDependsOnSizePainter : IWordsPainter
    {
        public Color[] GetColorsSequence(Dictionary<string, int> frequencyDictionary, Color startColor)
        {
            if (frequencyDictionary.Count == 0)
                return Array.Empty<Color>();
            var maxWordCount = frequencyDictionary.Values.Max();
            var resultA = startColor.A / maxWordCount;
            var colors = new Color[frequencyDictionary.Count];
            var counter = 0;
            foreach (var wordCount in frequencyDictionary.Values)
            {
                colors[counter] = Color.FromArgb(resultA * wordCount, startColor.R, startColor.G, startColor.B);
                counter++;
            }

            return colors;
        }
    }
}