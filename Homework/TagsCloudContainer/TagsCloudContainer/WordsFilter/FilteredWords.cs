using System.Collections.Generic;
using System.Linq;
using TagsCloudBuilder;
using TagsCloudContainer.WordsFilter.BoringWords;

namespace TagsCloudContainer.WordsFilter
{
    public class FilteredWords : IFilteredWords
    {
        private readonly int leftBound;
        private readonly int rightBound;
        public Dictionary<string, int> FilteredWordsList => new Dictionary<string, int>(words);
        private HashSet<string> boringWords;
        private Dictionary<string, int> words;

        public FilteredWords(IBoringWords boringWords, IWordsPreparer inputWords, int leftBound = 5, int rightBound = int.MaxValue)
        {
            this.boringWords = boringWords.GetBoringWords;
            words = inputWords.GetPreparedWords();
            this.leftBound = leftBound;
            this.rightBound = rightBound;

            RemoveBoringWords();
            RemoveWordsOutOfLengthRange();
        }

        private void RemoveBoringWords()
        {
            var lowerBoringWords = boringWords.Select(word => word.ToLower());

            words = words
                .Where(word => !lowerBoringWords.Contains(word.Key))
                .ToDictionary(word => word.Key, word => word.Value);
        }

        private void RemoveWordsOutOfLengthRange()
        {
            if (leftBound > rightBound)
                return;

            words = words
                .Where(word => word.Key.Length >= leftBound && word.Key.Length <= rightBound)
                .ToDictionary(word => word.Key, word => word.Value);
        }
    }
}
