using System.Collections.Generic;
using System.Linq;
using TagCloudContainerTests;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer
{
    public class WordsCountParser
    {
        public List<SimpleTag> Parse(string str)
        {
            var wordsCount = new Dictionary<string, int>();
            var words = str.Split("\n");
            foreach (var word in words) 
                wordsCount.AddOrIncreaseCount(word);
            return wordsCount.Keys
                .Select(word => new SimpleTag(word, wordsCount[word]))
                .ToList();
        }
    }
}