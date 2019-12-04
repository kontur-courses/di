using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.WordsPreprocessing
{
    class WordsAnalyzer
    {
        private IEnumerable<string> words;
        private Func<string, string> wordNormalizer;
        private Func<string, bool> isBoredWord;

        public WordsAnalyzer(IEnumerable<string> words, Func<string, string> wordNormalizer,
            Func<string, bool> isBoredWord)
        {
            this.words = words;
            this.wordNormalizer = wordNormalizer;
            this.isBoredWord = isBoredWord;
        }

        public Word[] AnalyzeAndGetWords(IEnumerable<string> words, int countToGet)
        {
            return GetNormalizeCountedWordsExcludingBoring(words)
                .OrderByDescending(w => w.Count)
                .Take(countToGet)
                .ToArray();
        }

        private IEnumerable<Word> GetNormalizeCountedWordsExcludingBoring(IEnumerable<string> words)
        {
            return null;
        }
    }
}
