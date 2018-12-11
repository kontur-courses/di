using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;
using TagsCloudContainer.TextPreprocessors.Filters;

namespace TagsCloudContainer.TextPreprocessors
{
    public class BasicWordsPreprocessor : IWordsPreprocessor
    {
        private readonly IEnumerable<IWordFilter> wordFilters;

        public BasicWordsPreprocessor(IEnumerable<IWordFilter> wordFilters)
        {
            this.wordFilters = wordFilters;
        }
        public IReadOnlyDictionary<string, int> PreprocessWords(IEnumerable<string> words)
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

                    if (lemma != null && WordMeetsAllRequirements(lemma))
                        preprocessedWords.Add(lemma.ToLower());
                }
            }
            return ProcessWords(preprocessedWords);
        }

        private bool WordMeetsAllRequirements(string lemma)
        {
            return wordFilters.All(e => e.Filter(lemma));
        }

        private IReadOnlyDictionary<string, int> ProcessWords(IEnumerable<string> preprocessWords)
        {
            var result = new Dictionary<string, int>();

            foreach (var word in preprocessWords)
            {
                result.TryGetValue(word, out var count);
                result[word] = count + 1;
            }

            return result;
        }
    }
}
