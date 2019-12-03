using System.Collections.Generic;
using TagsCloud.Interfaces;
using System.Linq;
    
namespace TagsCloud.WordProcessing
{
    public class DictionaryBasedCounter: IWordCounter
    {
        public IEnumerable<(string word, int frequency)> getAllStatistics(IEnumerable<string> words)
        {
            var wordFrequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!wordFrequency.ContainsKey(word))
                    wordFrequency[word] = 0;
                wordFrequency[word]++;
            }
            return wordFrequency.Select(pair => (pair.Key, pair.Value));
        }
    }
}
