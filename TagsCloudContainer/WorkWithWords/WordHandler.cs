using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TagsCloudContainer
{
    public class WordHandler
    {
        private readonly Dictionary<string, Word> _boringWords;
        private string[] _separators = {Environment.NewLine, ", ", ". ", " "};
        private string _pathToFile;
        private readonly string _projectDirectory;
        public WordHandler(string pathToFile, string boringWordsFileName = "")
        {
            _pathToFile = pathToFile;
            _projectDirectory = Directory.GetParent(Environment.CurrentDirectory)
                .Parent.Parent.FullName;
            var text = TextReader.GetTextFromFile(String.IsNullOrEmpty(boringWordsFileName)
                ? $"{_projectDirectory}\\TextFiles\\BoringWords.txt"
                : $"{_projectDirectory}\\TextFiles\\{boringWordsFileName}");

            _boringWords = CreateDictionaryBasedOnText(text);
        }

        public List<Word> ProcessWords(Settings settings)
        {
            var text = TextReader.GetTextFromFile(_pathToFile);
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