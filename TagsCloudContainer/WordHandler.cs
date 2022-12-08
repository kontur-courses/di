using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordHandler
    {
        private readonly HashSet<string> _boringWords;

        public WordHandler(string boringWordsFileName = "")
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory)
                .Parent.Parent.FullName;
            var text = TextReader.GetTextFromFile(String.IsNullOrEmpty(boringWordsFileName)
                ? $"{projectDirectory}\\BoringWords.txt"
                : $"{projectDirectory}\\{boringWordsFileName}");
            _boringWords = new HashSet<string>();
            foreach (var word in text.Split(Environment.NewLine))
                _boringWords.Add(word.ToLower());
        }

        public Dictionary<string, int> ProcessWords(string text)
        {
            var words = new Dictionary<string, int>();
            String[] separators = {Environment.NewLine, ", ", ". ", " "};
            foreach (var word in text.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            {
                var wordToDictionary = word.ToLower();
                if (word.Length <= 3 || _boringWords.Contains(wordToDictionary))
                    continue;
                if (!words.ContainsKey(wordToDictionary))
                    words.Add(wordToDictionary, 0);
                words[wordToDictionary]++;
            }

            return words
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}