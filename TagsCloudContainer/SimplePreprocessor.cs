using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class SimplePreprocessor : IPreprocessor
    {
        public IEnumerable<string> GetValidWords(IEnumerable<string> words)
        {
            var frequencyDictionary = GetFrequencyDictionary(words.Where(w => !GetForbiddenWords().Contains(w)));
            var validWords = frequencyDictionary
                .OrderBy(pair => pair.Value)
                .Reverse()
                .Select(pair => pair.Key);

            return validWords;
        }

        private Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 1;
                frequencyDictionary[word]++;
            }

            return frequencyDictionary;
        }

        private HashSet<string> GetForbiddenWords()
        {
            //надо будет переделать 
            //борать слова из файла (json?)
            return new HashSet<string>{
                "the",
                "a",
                "of",
                "his",
                "to",
                "in",
                "was",
                "and",
                "their",
                "its",
                "who",
                "as",
                "from",
                "we",
                "he",
                "they",
                "with",
                "is",
                "at",
                "for",
                "an",
                "are",
                "by",
                "have",
                "that",
                "also",
                "over",
                "get",
                "on",
                "most",
                "my",
                "it",
                "but",
                "be",
                "has",
                "i",
                "you",
                "so",
                "there",
                "us",
                "csn"
            };
        }
    }
}
