using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Words.FrequencyAnalyzers;

namespace App.Implementation.Words.FrequencyAnalyzers
{
    public class WordsFrequencyAnalyzer : IFrequencyAnalyzer
    {
        public Dictionary<string, double> AnalyzeWordsFrequency(IEnumerable<string> words)
        {
            var frequencies = new Dictionary<string, double>();
            var wordsCount = 0;

            foreach (var word in words)
            {
                if (frequencies.ContainsKey(word))
                    frequencies[word]++;
                else
                    frequencies[word] = 1;

                wordsCount++;
            }

            foreach (var word in frequencies.Keys.ToArray())
                frequencies[word] = Math.Round(frequencies[word] / wordsCount, 2);

            return frequencies;
        }
    }
}