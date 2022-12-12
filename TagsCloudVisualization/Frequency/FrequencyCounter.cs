using System.Collections.Generic;
using TagsCloudVisualization.Storages;

namespace TagsCloudVisualization.Frequency
{
    internal class FrequencyCounter : IFrequencyCounter
    {
        public IEnumerable<string> Elements { get; set; }

        public FrequencyCounter(IWordStorage storage)
        {
            Elements = storage.Words;
        }

        public Dictionary<string, int> GetFrequency()
        {
            var frequency = new Dictionary<string, int>();
            
            foreach (var element in Elements)
            {
                if (!frequency.ContainsKey(element))
                {
                    frequency.Add(element, 1);
                }
                else
                {
                    frequency[element]++;
                }
            }

            return frequency;
        }
    }
}
