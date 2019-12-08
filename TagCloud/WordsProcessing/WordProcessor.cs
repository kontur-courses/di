using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure;

namespace TagCloud.WordsProcessing
{
    public class WordProcessor : IWordProcessor
    {
        private readonly IWordSelector wordSelector;

        public WordProcessor(IWordSelector wordSelector)
        {
            this.wordSelector = wordSelector;
        }

        public IEnumerable<Word> PrepareWords(IEnumerable<string> rawWords)
        {
            return rawWords
                .Select(rawWord => rawWord.ToLower())
                .Where(word => wordSelector.IsSelectedWord(word))
                .Select(word => new Word(word));
        }
    }
}
