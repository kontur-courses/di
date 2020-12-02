using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloud.App
{
    public class WordFrequency
    {
        private readonly FileReaderProvider fileReaderProvider;
        private readonly WordChecker wordChecker;

        public WordFrequency(WordChecker wordChecker, FileReaderProvider fileReaderProvider)
        {
            this.wordChecker = wordChecker;
            this.fileReaderProvider = fileReaderProvider;
        }

        public Dictionary<string, double> GetFromFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            var lines = fileReaderProvider.GetFileReader(extension).ReadAllLines(filePath);
            var wordFrequencies = new Dictionary<string, double>();
            var words = lines
                .Select(x => x.ToLower())
                .Where(x => wordChecker.IsWordNotBoring(x));
            foreach (var word in words)
            {
                if (!wordFrequencies.ContainsKey(word))
                    wordFrequencies[word] = 1;
                wordFrequencies[word]++;
            }

            return wordFrequencies
                .ToDictionary(x => x.Key,
                    x => Math.Round(x.Value / lines.Length, 2));
        }
    }
}