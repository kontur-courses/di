using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using NHunspell;

namespace TagsCloudVisualization.WordProcessing
{
    public class WordAnalyzer
    {
        public WordsSettings WordsSettings { get; set; }
        private readonly Hunspell hunspell;
        public WordAnalyzer(WordsSettings wordsSettings)
        {
            WordsSettings = wordsSettings;
            hunspell = new Hunspell($"{AppDomain.CurrentDomain.BaseDirectory}/RuDictionary/affRu.aff",
                $"{AppDomain.CurrentDomain.BaseDirectory}/RuDictionary/dicRu.dic");
        }

        public Dictionary<string, int> MakeWordFrequencyDictionary()
        {
            IEnumerable<string> words = File.ReadAllLines(WordsSettings.PathToFile, Encoding.Default);
            words = FilterWords(words);
            var dictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (dictionary.ContainsKey(word))
                    dictionary[word]++;
                else dictionary.Add(word, 1);
            }

            return dictionary;
        }

        private IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words
                .Where(word => word.Length > 2 && hunspell.Spell(word))
                .Select(word => LeadToInitialForm(word).ToLower())
                .Where(word => word.Length != 0)
                .ToArray();
        }

        private string LeadToInitialForm(string word)
        {
            var initialForm = hunspell.Analyze(word).FirstOrDefault();
            return initialForm?.Substring(4).Split().FirstOrDefault() ?? "";
        }
    }
}