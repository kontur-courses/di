using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.WordsColoringAlgorithms
{
    public class DefaultWordsPainter : IWordsPainter
    {
        public Color[] GetColorsSequence(Dictionary<string, int> frequencyDictionary, Color startColor)
        {
            var wordsCount = frequencyDictionary.Count;
            if (wordsCount == 0|| startColor.IsEmpty)
                return Array.Empty<Color>();
            var colors = new Color[wordsCount];
            for (var i = 0; i < wordsCount; i++)
                colors[i] = startColor;
            return colors;
        }
    }
}