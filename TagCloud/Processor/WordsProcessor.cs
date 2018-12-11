using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Processor
{
    public class WordsProcessor : IWordsProcessor
    {
        private readonly HashSet<string> boringWords;

        public WordsProcessor(IEnumerable<string> boringWords)
        {
            this.boringWords = new HashSet<string>(boringWords);
        }

        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return words
                .Select(word => word.ToLower())
                .Where(word => !boringWords.Contains(word));
        }
    }
}