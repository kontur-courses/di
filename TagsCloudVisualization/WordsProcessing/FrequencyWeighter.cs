using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsProcessing
{
    public class FrequencyWeighter : IWeighter
    {
        public IEnumerable<WeightedWord> WeightWords(IEnumerable<string> words)
        {
            return words
                .GroupBy(s => s)
                .OrderByDescending(s => s.Count())
                .Select(g => new WeightedWord(g.Key, g.Count()));
        }
    }
}