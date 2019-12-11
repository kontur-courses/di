using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudGenerator.Tools
{
    public static class Helper
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

        public static string GetFileExtension(string path)
        {
            if (!path.Contains("."))
                throw new ArgumentException("invalid path");

            var part = path.Split('.');

            return part.Last().ToLower();
        }
    }
}