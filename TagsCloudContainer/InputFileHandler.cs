using System;
using System.Collections.Generic;
using TagsCloudContainer.FileOpeners;

namespace TagsCloudContainer
{
    /// <summary>
    /// Обрабатывает входной файл. На выходе - частотный словарь без скучных слов.
    /// </summary>
    public class InputFileHandler
    {
        private readonly IFileReader fileReader;

        public InputFileHandler(IFileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        public Dictionary<string, int> FormFrequencyDictionary(string filePath)
        {
            var words = FileToWordsArray(filePath);
            var filteredWords = BoringWordsDeleter.DeleteBoringWords(words);
            var frequencyDict = MakeWordsFrequencyDictionary(filteredWords);
            return frequencyDict;
        }

        private string[] FileToWordsArray(string filePath)
        {
            var input = fileReader.ReadFile(filePath);
            if (input.Length == 0)
                throw new ArgumentException("Empty file");
            return input.Split(new[] {Environment.NewLine, " "}, StringSplitOptions.None);
        }

        private Dictionary<string, int> MakeWordsFrequencyDictionary(IEnumerable<string> words)
        {
            var wordOccurrences = new Dictionary<string, int>();
            foreach (var word in words)
                wordOccurrences[word.ToLower()] = wordOccurrences.ContainsKey(word.ToLower())
                    ? wordOccurrences[word.ToLower()] + 1
                    : 1;
            return wordOccurrences;
        }
    }
}