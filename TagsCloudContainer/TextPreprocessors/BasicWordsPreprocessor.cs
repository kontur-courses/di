using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;

namespace TagsCloudContainer.TextPreprocessors
{
    public class BasicWordsPreprocessor : IWordsPreprocessor
    {
        public IEnumerable<KeyValuePair<string, int>> PreprocessWords(IEnumerable<string> words,
            IEnumerable<string> wordsToBeExcluded = null)
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "HunspellDicts", "Russian");
            var affFile = Path.Combine(dir, "ru.aff");
            var dictFile = Path.Combine(dir, "ru.dic");

            var preprocessedWords = new List<string>();

            using (var hunspell = new Hunspell(affFile, dictFile))
            {
                foreach (var word in words)
                {
                    var lemma = hunspell.Stem(word).FirstOrDefault();

                    // Заглушка. TODO: исключать скучные слова по части речи?
                    if (lemma?.Length > 4)
                        preprocessedWords.Add(lemma.ToLower());
                }
            }
            return ProcessWords(preprocessedWords, wordsToBeExcluded);
        }

        private IEnumerable<KeyValuePair<string, int>> ProcessWords(IEnumerable<string> preprocessWords,
            IEnumerable<string> wordsToBeExcluded)
        {
            var result = new Dictionary<string, int>();

            foreach (var word in preprocessWords)
            {
                result.TryGetValue(word, out var count);
                result[word] = count + 1;
            }
            
            if (wordsToBeExcluded != null)
                foreach (var word in wordsToBeExcluded)
                    result.Remove(word);

            return result.OrderByDescending(e => e.Value);
        }
    }
}
