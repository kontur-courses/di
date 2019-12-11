using System.Collections.Generic;

namespace TagsCloudGenerator.FileReaders
{
    public static class WordToCountConverter
    {
        public static Dictionary<string, int> GetWordToCount(List<string> words)
        {
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