using System.Collections.Generic;
using System.Linq;
using TagsCloudBuilder;
using TagsCloudContainer.WordsFilter.BannedWords;

namespace TagsCloudContainer.WordsFilter
{
    public class WordsFilters : IWordsFilters
    {
        public Dictionary<string, int> FilteredWords => new Dictionary<string, int>(words);
        private HashSet<string> bannedWords;
        private Dictionary<string, int> words;

        public WordsFilters(IBannedWords bannedWords, IWordsPreparer inputWords)
        {
            this.bannedWords = bannedWords.GetBannedWords;
            this.words = inputWords.GetPreparedWords();
        }

        public void RemoveIgnoredWords()
        {
            var lowerBoringWords = bannedWords.Select(word => word.ToLower());

            words = words
                .Where(word => !lowerBoringWords.Contains(word.Key))
                .ToDictionary(word => word.Key, word => word.Value);
        }

        public void RemoveWordsOutOfLengthRange(int leftBound, int rightBound = int.MaxValue)
        {
            if (leftBound > rightBound)
                return;

            words = words
                .Where(word => word.Key.Length >= leftBound && word.Key.Length <= rightBound)
                .ToDictionary(word => word.Key, word => word.Value);
        }
    }
}
