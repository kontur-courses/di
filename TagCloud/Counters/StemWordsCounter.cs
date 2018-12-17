using System;
using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagCloud
{
    public class StemWordsCounter: IWordsCounter
    {
        private readonly Dictionary<string, int> countedWords = new Dictionary<string, int>();

        public IReadOnlyDictionary<string, int> CountedWords => countedWords;

        public void UpdateWith(string text)
        {
            var words = text
                .ToLower()
                .Split(" \t\n\r.,?!:;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            using (var hsl = new Hunspell("en_us.aff", "en_us.dic"))
                foreach (var word in words)
                    IncOrAddToCounter(hsl.Stem(hsl.Suggest(word).First()).First());
        }

        private void IncOrAddToCounter(string word)
        {
            if (countedWords.ContainsKey(word))
                countedWords[word] += 1; 
            else
                countedWords[word] = 1;
        }
    }
}