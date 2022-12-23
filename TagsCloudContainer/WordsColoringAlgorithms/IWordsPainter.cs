using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.WordsColoringAlgorithms
{
    public interface IWordsPainter
    {
        public Dictionary<string, Color> GetWordColorDictionary(Dictionary<string, int> frequencyDictionary,
            Color startColor);
    }
}