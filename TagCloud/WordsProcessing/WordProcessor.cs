using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure;

namespace TagCloud.WordsProcessing
{
    public class WordProcessor : IWordProcessor
    {
        private readonly IWordSelector wordSelector;
        private readonly IWordCounter wordCounter;

        public WordProcessor(IWordSelector wordSelector, IWordCounter wordCounter)
        {
            this.wordSelector = wordSelector;
            this.wordCounter = wordCounter;
        }

        public IEnumerable<Word> PrepareWords(IEnumerable<string> rawWords)
        {
            var convertedWords = rawWords
                .Select(rawWord => rawWord.ToLower())
                .Select(word => new Word(word));
            var countedWords = wordCounter.GetCountedWords(convertedWords);
            return countedWords
                .Where(word => wordSelector.IsSelectedWord(word));

        }
    }
}
