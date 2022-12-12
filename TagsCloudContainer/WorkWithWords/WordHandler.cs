using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.TextReaders;

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
            if (settings.BoringWordsFileName != String.Empty)
                SetUpBoringWords();
        }

        private void SetUpBoringWords()
        {
            var reader = TextReaderGenerator.GetReader(_settings.BoringWordsFileName);
            var text = reader.GetTextFromFile(_settings.BoringWordsFileName);

            _boringWords = CreateDictionaryBasedOnText(text);
        }

        public List<Word> ProcessWords()
        {
            var reader = TextReaderGenerator.GetReader(_settings.FileName);
            var text = reader.GetTextFromFile(_settings.FileName);

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