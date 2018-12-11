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
            var frequencyDictionary = GetFrequencyDictionary(words.Where(w => !forbiddenWords.Contains(w)));
            var validWords = frequencyDictionary
                .OrderBy(pair => pair.Value)
                .Reverse()
                .Select(pair => pair.Key);

            return validWords;
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

        private Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 1;
                frequencyDictionary[word]++;
            }

            return frequencyDictionary;
        }

        private HashSet<string> GetForbiddenWords()
        {
            return wordExcluder.GetExcludedWords();
        }
    }
}