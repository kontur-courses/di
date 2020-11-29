using System.Collections.Generic;
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
                boringWords = GetWordsFromTextFile(filesSettings.BoringWordsFileName).ToHashSet();
            var result = new Dictionary<string, int>();
            var words = text == null ? GetWordsFromTextFile(filesSettings.TextFileName) : GetWordsFromText(text);
            foreach (var word in words)
                if (!boringWords.Contains(word))
                    result[word] = result.ContainsKey(word) ? result[word] + 1 : 1;
            return result;
        }

        private IEnumerable<string> GetWordsFromTextFile(string fileName)
        {
            var filePath = Path.GetFullPath(fileName);
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