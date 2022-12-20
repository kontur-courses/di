using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.WordsColoringAlgorithms
{
    public class GradientWordsPainter : IWordsPainter
    {
        public Color[] GetColorsSequence(Dictionary<string, int> frequencyDictionary, Color startColor)
        {
            var counter = 0;
            if (frequencyDictionary.Count == 0)
                return Array.Empty<Color>();
            var colors = new Color[frequencyDictionary.Count];
            while (counter < frequencyDictionary.Count)
            {
                var resultA = startColor.A - 10 > 0 ? startColor.A - 10 : 0;
                startColor = Color.FromArgb(resultA, startColor.R, startColor.G, startColor.B);
                colors[counter] = startColor;
                counter++;
            }

            return colors;
        }

       
    }
}