using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class FrequencyCounter : IFrequencyCounter
    {
        public Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            var freqDictionary = new Dictionary<string, int>();

            foreach (var word in words.Where(word => word != null))
                if (freqDictionary.ContainsKey(word))
                    freqDictionary[word]++;
                else
                    freqDictionary[word] = 1;

            return freqDictionary;
        }
    }
}