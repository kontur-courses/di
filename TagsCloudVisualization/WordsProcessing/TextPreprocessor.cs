using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsProcessing
{
    public class TextPreprocessor : IWordsProvider
    {
        private readonly IEnumerable<string> words;
        private readonly IFilter[] wordsFilters;
        private readonly IWordsChanger[] wordsChangers;


        public TextPreprocessor(IWordsProvider wordsProvider, IFilter[] wordsFilters, IWordsChanger[] wordsChangers)
        {
            words = wordsProvider.Provide();
            this.wordsFilters = wordsFilters;
            this.wordsChangers = wordsChangers;
        }

        public IEnumerable<string> Provide()
        {
            var filteredWords = wordsFilters.Aggregate(words, (curWords, filter) => filter.FilterWords(curWords));
            var changedWords = wordsChangers
                .Aggregate(filteredWords, (curWords, changer) => changer.ChangeWords(curWords));
            return changedWords;
        }
    }
}