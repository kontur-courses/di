using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class WordPreprocessor
    {
        private readonly IEnumerable<string> words;
        
        public WordPreprocessor(IEnumerable<string> words)
        {
            this.words = words;
        }

        public IEnumerable<string> GetPreprocessedWords(IEnumerable<ITextFilter> filters)
        {
            var preprocessedWords = words.GetLowerCaseWords();
            return filters.Aggregate(preprocessedWords, (current, textFilter) => current.GetFilteredWords(textFilter));
        }
    }
}