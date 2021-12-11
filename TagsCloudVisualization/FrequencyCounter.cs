#region

using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Interfaces;

#endregion

namespace TagsCloudVisualization
{
    public class FrequencyCounter : IFrequencyCounter
    {
        public Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentException("Words was null");

            var freqDictionary = new Dictionary<string, int>();

            foreach (var word in words.Where(word => word != null))
                freqDictionary.AddOrUpdate(word, 1, x => freqDictionary[word] + 1);

            return freqDictionary;
        }
    }
}