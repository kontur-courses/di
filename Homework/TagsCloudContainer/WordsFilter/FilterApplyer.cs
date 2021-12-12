using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.WordsFilter
{
    public class FilterApplyer : IFilterApplyer
    {
        private readonly IWordsFilter[] wordsFilters;

        public FilterApplyer(IWordsFilter[] wordsFilters)
        {
            this.wordsFilters = wordsFilters;
        }

        public ICollection<string> Apply(ICollection<WordInfo> words)
        {
            return wordsFilters
                .Aggregate(words, (current, implementation) => implementation.Filter(current))
                .Select(wordInfo => wordInfo.Lemma)
                .ToArray();
        }
    }
}