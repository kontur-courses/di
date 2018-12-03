using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagsCloudContainer.TextPreprocessors
{
    class BasicWordsPreprocessor : IWordsPreprocessor
    {
        public IEnumerable<string> PreprocessWords(string[] words)
        {
            //TODO: вынести файлы в ресурсы
            using (var hunspell = new Hunspell("ru.aff", "ru.dic"))
            {
                foreach (var word in words)
                {
                    //TODO: настроить нормальное считывание файла
                    var lemma = hunspell.Stem(word.TrimEnd('\r')).FirstOrDefault();

                    // Заглушка. TODO: исключать скучные слова по части речи?
                    if (lemma?.Length > 4)
                        yield return lemma.ToLower();
                }
                
            }
        }
    }
}
