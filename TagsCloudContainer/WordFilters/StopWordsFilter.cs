using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class StopWordsFilter : IWordsFilter
    {
        private StopWords stopWords;

        public StopWordsFilter(StopWords stopWords)
        {
            this.stopWords = stopWords;
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower()).Where(word => !stopWords.Contains(word)).ToList();
        }
    }
}
