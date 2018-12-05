using System.Collections.Generic;
using System.Linq;
using NHunspell;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class BasicFormConverter : IWordProcessor
    {
        public IEnumerable<string> Preprocess (IEnumerable<string> words)
        {
            List<string> wordsInBasicForm;
            using (var hunspell = new Hunspell("Dictionaries/ru_RU.aff", "Dictionaries/ru_RU.dic"))
            {
                wordsInBasicForm = (from word in words
                    select hunspell.Stem(word).Any() 
                        ? hunspell.Stem(word).First() 
                        : word).ToList();

            }
            return wordsInBasicForm;
        }
    }
}