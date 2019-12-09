using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure;

namespace TagCloud.WordsProcessing
{
    public class WordCounter : IWordCounter
    {
        public IEnumerable<Word> GetCountedWords(IEnumerable<Word> words)
        {
            return words
                .GroupBy(w => w)
                .Select(g => g.Key.SetCount(g.Count()));
        }
    }
}