using System;
using System.Linq;

namespace Visualization.Preprocessors
{
    public class RemovingBoringWordsPreprocessor : IWordsPreprocessor
    {
        private readonly int minWordLength;

        public RemovingBoringWordsPreprocessor(int minWordLength)
        {
            if (minWordLength <= 0)
                throw new ArgumentException("Must be positive", nameof(minWordLength));
            this.minWordLength = minWordLength;
        }

        public string[] Preprocess(string[] rawWords)
        {
            return rawWords
                .Where(word => !IsBoring(word))
                .ToArray();
        }

        private bool IsBoring(string word)
        {
            return word.Length < minWordLength;
        }
    }
}