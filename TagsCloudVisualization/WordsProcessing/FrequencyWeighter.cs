using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsProcessing
{
    public class FrequencyWeighter : IWeighter
    {
        private readonly IWordsProvider wordsProvider;
        public FrequencyWeighter(IWordsProvider wordsProvider)
        {
            this.wordsProvider = wordsProvider;
        }

        public IEnumerable<WeightedWord> WeightWords()
        {
            return wordsProvider.Provide()
                .GroupBy(s => s)
                .OrderByDescending(s => s.Count())
                .Select(g => new WeightedWord(g.Key, g.Count()));
        }
    }
}