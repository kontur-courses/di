using System;
using System.Collections.Generic;

namespace TagsCloudContainer.TextProcessing
{
    public static class WordsFrequency
    {
        public static Dictionary<string, int> GetWordsFrequency(string[] words)
        {
            if (words == null || words.Length == 0)
                throw new ArgumentException("Array of words must be not null and not empty");
            var frequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (frequency.ContainsKey(word))
                    frequency[word]++;
                else
                    frequency[word] = 1;
            }

            return frequency;
        }
    }
}