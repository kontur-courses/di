using System.Collections.Generic;
using System.Linq;

namespace TagCloud.WordProcessing
{
    public class WordProcessor : IWordProcessor
    {
        private readonly IWordSelector wordSelector;

        public WordProcessor(IWordSelector wordSelector)
        {
            this.wordSelector = wordSelector;
        }

        public IEnumerable<string> PrepareWords(IEnumerable<string> rawWords)
        {
            return rawWords
                .Select(rawWord => rawWord.ToLower())
                .Where(word => wordSelector.IsSelectedWord(word));
        }
    }
}
