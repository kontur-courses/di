using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudGenerator.WordsHandler.Filters
{
    public class BoringWordsEjector : IWordsFilter
    {
        private HashSet<string> boringWords;
        
        public void AddBoringWords(List<string> words)
        {
            if (boringWords == null)
                boringWords = new HashSet<string>();

            foreach (var word in words)
                boringWords.Add(word);
        }

        public Dictionary<string, int> Filter(Dictionary<string, int> wordToCount)
        {
            if (wordToCount == null)
                throw new ArgumentNullException("there are no words to filter");

            return wordToCount
                .Where(pair => !boringWords.Contains(pair.Key))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}