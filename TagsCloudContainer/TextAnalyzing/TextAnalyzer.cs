﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.TextAnalyzing
{
    public class TextAnalyzer
    {
        private readonly FilesSettings filesSettings;
        private string text;
        private IEnumerable<string> boringWords;
        private string pathToTextFile;
        private string pathToBoringWordsFile;

        public TextAnalyzer(FilesSettings filesSettings)
        {
            this.filesSettings = filesSettings;
        }

        public TextAnalyzer(string text, string boringWords)
        {
            this.text = text;
            this.boringWords = GetWordsFromText(boringWords);
        }

        public Dictionary<string, int> GetWordWithFrequency()
        {
            if(boringWords == null) 
                boringWords = GetWordsFromTextFile(filesSettings.BoringWordsFilePath).ToHashSet();
            var result = new Dictionary<string, int>();
            var words = text == null ? GetWordsFromTextFile(filesSettings.TextFilePath) : GetWordsFromText(text);
            foreach (var word in words)
                if (!boringWords.Contains(word))
                    result[word] = result.ContainsKey(word) ? result[word] + 1 : 1;
            return result;
        }

        private IEnumerable<string> GetWordsFromTextFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                var text = reader.ReadToEnd().ToLower();
                return GetWordsFromText(text);
            }
        }

        private IEnumerable<string> GetWordsFromText(string text)
        {
            var wordPattern = new Regex("\\b[a-z]+\\b");
            var wordsMatches = wordPattern.Matches(text);
            foreach (Match wordMatch in wordsMatches) yield return wordMatch.Value;
        }
    }
}