using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordHandler
    {
        private readonly Dictionary<string, Word> _boringWords;
        private string[] _separators = {Environment.NewLine, ", ", ". ", " "};

        public WordHandler(string boringWordsFileName = "")
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory)
                .Parent.Parent.FullName;
            var text = TextReader.GetTextFromFile(String.IsNullOrEmpty(boringWordsFileName)
                ? $"{projectDirectory}\\TextFiles\\BoringWords.txt"
                : $"{projectDirectory}\\TextFiles\\{boringWordsFileName}");

            _boringWords = CreateDictionaryBasedOnText(text);
        }

        public List<Word> ProcessWords(string text, Settings settings)
        {
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