using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudGenerator.WordsHandler
{
    public class BoringWordsEjector : IWordsFilter
    {
        private readonly string[] boringWords;

        public BoringWordsEjector(string[] boringWords)
        {
            this.boringWords = boringWords;
        }

        public Dictionary<string, int> Filter(Dictionary<string, int> wordToCount)
        {
            if (wordToCount == null)
                throw new ArgumentNullException();

            return wordToCount
                .Where(pair => !boringWords.Contains(pair.Key))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}