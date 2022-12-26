using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudContainer.Interfaces;

namespace TagCloudContainer.TagSorters
{
    public class RandomFrequencySorter : IFrequencySorter
    {
        public Dictionary<string, int> GetSortedWordsWithFrequancies(Dictionary<string, int> frequencyDict)
        {
            var rand = new Random((int)(DateTime.Now.Ticks % int.MaxValue));

            return frequencyDict
                .OrderByDescending(x => rand.Next())
                .ToDictionary(
                    x => x.Key,
                    x => x.Value
                );
        }
    }
}
