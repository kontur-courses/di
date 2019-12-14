using System;
using System.Collections.Generic;

namespace TagsCloudGenerator.Tools
{
    public static class ParseHelper
    {
        public static Dictionary<string, int> GetWordToCount(List<string> words)
        {
            if (words == null)
                throw new ArgumentNullException("words is null");

            var wordToCount = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (!wordToCount.ContainsKey(word))
                    wordToCount.Add(word, 0);

                wordToCount[word]++;
            }

            return wordToCount;
        }
    }
}