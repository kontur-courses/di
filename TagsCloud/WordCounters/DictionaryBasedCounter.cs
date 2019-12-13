using System.Collections.Generic;
using TagsCloud.Interfaces;
using System.Linq;
    
namespace TagsCloud.WordCounters
{
    public class DictionaryBasedCounter: IWordCounter
    {
        public IEnumerable<(string word, int frequency)> GetWordsStatistics(IEnumerable<string> words) => words.GroupBy(word => word).Select(group => (group.Key, group.Count()));
    }
}
