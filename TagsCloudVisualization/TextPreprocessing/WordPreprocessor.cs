using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class WordPreprocessor
    {
        private readonly IEnumerable<string> words;
        private readonly IEnumerable<ITextFilter> filters;
        
        public WordPreprocessor(IEnumerable<string> words, IEnumerable<ITextFilter> filters)
        {
            this.words = words;
            this.filters = filters;
        }

        public IEnumerable<string> GetPreprocessedWords()
        {
            var preprocessedWords = words.Select(word => word.ToLower());
            return filters.Aggregate(preprocessedWords, (current, textFilter) => textFilter.FilterWords(current));
        }
    }
}