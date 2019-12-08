using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure;

namespace TagCloud.WordsPreparation
{
    public class WordCountSetter : IWordCountSetter
    {
        public IEnumerable<Word> GetCountedWords(IEnumerable<Word> words)
        {
            return words
                .GroupBy(w => w)
                .Select(g => g.Key.WithCount(g.Count()));
        }
    }
}