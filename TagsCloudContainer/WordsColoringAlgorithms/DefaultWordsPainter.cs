using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.WordsColoringAlgorithms
{
    public class DefaultWordsPainter : IWordsPainter
    {
        public Dictionary<string, Color> GetWordColorDictionary(Dictionary<string, int> frequencyDictionary,
            Color startColor)
        {
            var result = new Dictionary<string, Color>();
            if (frequencyDictionary.Count == 0)
                return result;
            foreach (var pair in frequencyDictionary)
                result[pair.Key] = startColor;
            return result;
        }
    }
}