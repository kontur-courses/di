using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class WordFrequency
    {
        private readonly IWordChecker wordChecker;

        public WordFrequency(IWordChecker wordChecker)
        {
            this.wordChecker = wordChecker;
        }

        public Dictionary<string, double> Get(string[] lines)
        {
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