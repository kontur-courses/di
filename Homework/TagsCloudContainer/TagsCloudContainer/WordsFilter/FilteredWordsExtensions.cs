using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.WordsFilter
{
    public static class FilteredWordsExtensions
    {

        public static Dictionary<string, int> RemoveWord(this Dictionary<string, int> words, string boringWord)
        {
            return words
                .Where(word => word.Key != boringWord.ToLower())
                .ToDictionary(word => word.Key, word => word.Value);
        }

        public static Dictionary<string, int> RemoveWords(this Dictionary<string, int> words, List<string> boringWords)
        {
            var lowerBoringWords = boringWords.Select(word => word.ToLower());

            return words
                .Where(word => !lowerBoringWords.Contains(word.Key))
                .ToDictionary(word => word.Key, word => word.Value);
        }


        public static Dictionary<string, int> RemoveWordsOutOfLengthRange(this Dictionary<string, int> words,
            int leftBound, int rightBound = int.MaxValue)
        {
            if (leftBound > rightBound)
                return words;

            return words
                .Where(word => word.Key.Length >= leftBound && word.Key.Length <= rightBound)
                .ToDictionary(word => word.Key, word => word.Value);
        }
    }
}
