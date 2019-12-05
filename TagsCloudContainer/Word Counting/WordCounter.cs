using System.Collections.Generic;

namespace TagsCloudContainer.Word_Counting
{
    public class WordCounter : IWordCounter
    {
        private readonly IWordFilter filter;
        private readonly IWordNormalizer normalizer;
        public WordCounter(IWordFilter filter, IWordNormalizer normalizer)
        {
            this.filter = filter;
            this.normalizer = normalizer;
        }
        public Dictionary<string, int> CountWords(IEnumerable<string> words)
        {
            throw new System.NotImplementedException();
        }
    }
}