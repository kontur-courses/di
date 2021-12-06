using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer
{
    public class WordsCountParser
    {
        public Dictionary<string, int> Parse(string str)
        {
            var wordsCount = new Dictionary<string, int>();
            var words = GetWordsWithoutUseless(str.Split("\n"));
            foreach (var word in words) 
                wordsCount.AddOrIncreaseCount(word);
            return wordsCount;
        }

        private IEnumerable<string> GetWordsWithoutUseless(IEnumerable<string> words)
        {
            // Extensions -? 
            // Не уверен как это реализовать, в лоб не хочется, так что пока по минимуму
            return words.Where(w => w.Length > 2);
        }
    }
}