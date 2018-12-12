using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using NHunspell;

namespace TagsCloudPreprocessor
{
    public class SimpleWordsValidator : IWordsValidator
    {
        //ToDo Создать класс, где слова будут приводиться к одной форме, там же будет исключение разных частей речи
        private IWordExcluder wordExcluder;

        public SimpleWordsValidator(IWordExcluder wordExcluder)
        {
            this.wordExcluder = wordExcluder;
        }

        public IEnumerable<string> GetValidWords(IEnumerable<string> words)
        {
            words = GetWordsStem(words);
            var forbiddenWords = GetForbiddenWords();
            return words.Where(w=>!forbiddenWords.Contains(w));
        }

        private IEnumerable<string> GetWordsStem(IEnumerable<string> words)
        {
            var h = new Hunspell(@"ru_RU.aff", @"ru_RU.dic");
            
            return words
                .Select(word => h.Stem(word))
                .Select(stem => stem.FirstOrDefault())
                .Where(wordStem => wordStem != null)
                .ToList();
        }

        private HashSet<string> GetForbiddenWords()
        {
            return wordExcluder.GetExcludedWords();
        }
    }
}