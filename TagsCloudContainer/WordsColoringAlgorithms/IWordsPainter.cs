using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.WordsColoringAlgorithms
{
    public interface IWordsPainter
    {
        public Color[] GetColorsSequence(Dictionary<string, int> frequencyDictionary, string startColor);
    }
}