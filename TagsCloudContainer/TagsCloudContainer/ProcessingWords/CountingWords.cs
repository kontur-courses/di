using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.ProcessingWords
{
    public static class CountingWords
    {
        public static IEnumerable<(string, int)> GetWordAndNumberOfRepetitions(IEnumerable<string> words)
        {
            var frequencyOfWords = new Dictionary<string, int>();
            foreach (var word in words)
                frequencyOfWords[word] = frequencyOfWords.ContainsKey(word) ? frequencyOfWords[word] + 1 : 1;

            var random = new Random();
            var wordsRandomSort = frequencyOfWords.Keys.ToArray();

            for (var i = 0; i < wordsRandomSort.Length - 1; i += 1)
            {
                var swapIndex = random.Next(i, wordsRandomSort.Length);
                if (swapIndex == i) continue;
                var temp = wordsRandomSort[i];
                wordsRandomSort[i] = wordsRandomSort[swapIndex];
                wordsRandomSort[swapIndex] = temp;
            }

            foreach (var word in wordsRandomSort)
                yield return (word, frequencyOfWords[word]);
        }
    }
}