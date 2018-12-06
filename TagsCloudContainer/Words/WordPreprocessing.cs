using NHunspell;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordPreprocessing
    {
        public IEnumerable<string> Words { get; private set; }

        public WordPreprocessing(string[] words)
        {
            Words = words;
        }

        public WordPreprocessing ToLower()
        {
            Words = Words.Select(w => w.ToLower());
            return this;
        }

        public WordPreprocessing IgnoreInvalidWords()
        {
            using (Hunspell hunspell = new Hunspell("ru_RU.aff", "ru_RU.dic"))
            {
                //Words = Words.Where(w => hunspell.Spell(w)); // Hunspell выдает какие-то странные ошибки в linq
                //Words = Words.Select(w => hunspell.Stem(w)[0]).Where(w => !string.IsNullOrEmpty(w));
                var newWords = new List<string>();
                foreach (var word in Words)
                {
                    var stem = hunspell.Stem(word);
                    if (stem.Count > 0)
                        newWords.Add(stem[0]);
                }

                Words = newWords;
            }
            return this;
        }

        public WordPreprocessing Exclude(HashSet<string> wordsToExclude)
        {
            Words = Words.Where(w => !wordsToExclude.Contains(w));
            return this;
        }

        public WordPreprocessing CustomPreprocessingSelect(Func<string, string> func)
        {
            Words = Words.Select(func);
            return this;
        }

        public WordPreprocessing CustomPreprocessingWhere(Func<string, bool> func)
        {
            Words = Words.Where(func);
            return this;
        }
    }
}
