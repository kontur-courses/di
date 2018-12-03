using System;
using System.Collections.Generic;
using System.Linq;
using NHunspell;
using TagsCloudContainer.SourceTextReaders;

namespace TagsCloudContainer.TextPreprocessors
{
    class BasicWordsPreprocessor : IWordsPreprocessor
    {
        public IEnumerable<string> PreprocessWords(string[] words)
        {
            using (var hunspell = new Hunspell("ru.aff", "ru.dic"))
            {
                foreach (var word in words)
                {
                    var lemma = hunspell.Stem(word).FirstOrDefault();

                    // Заглушка. TODO: исключать скучные слова по части речи
                    if (lemma?.Length > 4)
                        yield return lemma.ToLower();
                }
                
            }
        }
    }
}
