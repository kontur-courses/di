using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.WorkWithWords
{
    public class WordHandler
    {
        private Dictionary<string, Word> _boringWords;
        private Settings _settings;
        private string[] _separators = {Environment.NewLine, ", ", ". ", " "};

        public WordHandler(Settings settings)
        {
            _settings = settings;
        }

        public void SetUpBoringWords(string text)
        {
            _boringWords = CreateDictionaryBasedOnText(text);
        }

        public List<Word> ProcessWords(string text)
        {
            var wordsDictionary = CreateDictionaryBasedOnText(text);
            var words = wordsDictionary
                .Select(x => x.Value)
                .Where(x => !_boringWords.ContainsKey(x.Value) && x.Value.Length >= 3)
                .ToList();

            foreach (var word in words)
                word.GenerateSize(_settings, words.Count);

            return words;
        }

        private Dictionary<string, Word> CreateDictionaryBasedOnText(string text)
        {
            var dictionary = new Dictionary<string, Word>();
            foreach (var word in text.Split(_separators, StringSplitOptions.RemoveEmptyEntries))
            {
                var wordToDictionary = word.ToLower();
                if (!dictionary.ContainsKey(wordToDictionary))
                    dictionary.Add(wordToDictionary, new Word(wordToDictionary));
                else
                    dictionary[wordToDictionary].Count++;
            }

            return dictionary;
        }
    }
}