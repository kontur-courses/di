using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;

namespace TagsCloudContainer.TextPreprocessors
{
    public class BasicWordsPreprocessor : IWordsPreprocessor
    {
        public IEnumerable<string> PreprocessWords(string[] words)
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "HunspellDicts", "Russian");
            var affFile = Path.Combine(dir, "ru.aff");
            var dictFile = Path.Combine(dir, "ru.dic");
            using (var hunspell = new Hunspell(affFile, dictFile))
            {
                foreach (var word in words)
                {
                    var lemma = hunspell.Stem(word).FirstOrDefault();

                    // Заглушка. TODO: исключать скучные слова по части речи?
                    if (lemma?.Length > 4)
                        yield return lemma.ToLower();
                }

            }
        }
    }
}
