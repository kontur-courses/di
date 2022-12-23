using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.WordsColoringAlgorithms
{
    public class GradientDependsOnSizePainter : IWordsPainter
    {
        public Dictionary<string, Color> GetWordColorDictionary(Dictionary<string, int> frequencyDictionary,
            Color startColor)
        {
            var result = new Dictionary<string, Color>();
            if (frequencyDictionary.Count == 0)
                return result;
            var maxWordCount = frequencyDictionary.Values.Max();
            var resultA = startColor.A / maxWordCount;
            foreach (var pair in frequencyDictionary)
            {
                var wordCount = pair.Value;
                result[pair.Key] = Color.FromArgb(resultA * wordCount, startColor.R, startColor.G, startColor.B);
            }

            return result;
        }
    }
}