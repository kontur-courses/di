using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudContainer.Interfaces;

namespace TagCloudContainer.FrequencySorters
{
    public class DefaultFrequencySorter : IFrequencySorter
    {
        public Dictionary<string, int> GetSortedWordsWithFrequancies(Dictionary<string, int> frequencyDict)
        {
            return frequencyDict
                .OrderByDescending(x => x.Value)
                .ToDictionary(
                    x => x.Key,
                    x => x.Value
                );
        }
    }
}
