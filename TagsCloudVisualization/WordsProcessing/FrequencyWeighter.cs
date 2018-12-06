using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class FrequencyWeighter : IWeighter
    {
        public IEnumerable<WeightedWord> WeightWords(IWordsProvider wordsProvider)
        {
            return wordsProvider.GetWords()
                .GroupBy(s => s)
                .OrderByDescending(s => s.Count())
                .Select(g => new WeightedWord(g.Key, g.Count()));
        }
    }
}