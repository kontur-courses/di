using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;

namespace TagCloud
{
    public class SimpleWordsCounter: IWordsCounter
    {
        private readonly Dictionary<string, int> countedWords = new Dictionary<string, int>();

        public IReadOnlyDictionary<string, int> CountedWords => countedWords;

        public void UpdateWith(string text)
        {
            var words = text
                .ToLower() //TODO get punctuation string
                .Split(" \t\n\r.,?!:;".ToCharArray(),StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            foreach (var word in words)
                if (countedWords.ContainsKey(word))
                    countedWords[word] += 1; //TODO make better
                else
                    countedWords[word] = 1;
        }
    }
}