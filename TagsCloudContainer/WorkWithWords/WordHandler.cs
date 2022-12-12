using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordHandler
    {
        private Dictionary<string, Word> _boringWords;
        private ITextReader _reader;
        private string[] _separators = {Environment.NewLine, ", ", ". ", " "};
        private string _fileName;
        private readonly string _projectDirectory;

        public WordHandler(ITextReader reader, Settings settings)
        {
            _fileName = settings.FileName;
            _reader = reader;
            _projectDirectory = Directory.GetParent(Environment.CurrentDirectory)
                .Parent.Parent.FullName;
            SetUpBoringWords(settings.BoringWordsFileName);
        }

        public void SetUpBoringWords(string boringWordsFileName)
        {
            var text = _reader.GetTextFromFile(String.IsNullOrEmpty(boringWordsFileName)
                ? $"{_projectDirectory}\\TextFiles\\BoringWords.txt"
                : $"{_projectDirectory}\\TextFiles\\{boringWordsFileName}");

            _boringWords = CreateDictionaryBasedOnText(text);
        }

        public List<Word> ProcessWords(Settings settings)
        {
            var text = _reader.GetTextFromFile($"{_projectDirectory}\\TextFiles\\{_fileName}");
            var wordsDictionary = CreateDictionaryBasedOnText(text);
            var words = wordsDictionary
                .Select(x => x.Value)
                .Where(x => !_boringWords.ContainsKey(x.Value) && x.Value.Length >= 3)
                .ToList();

            foreach (var word in words)
                word.GenerateSize(settings, words.Count);

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