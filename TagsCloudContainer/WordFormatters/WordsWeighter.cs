using System.Collections.Generic;

namespace TagsCloudContainer.WordFormatters
{
    public class WordsWeighter : IWordsWeighter
    {
        public IDictionary<string, int> GetWordsWeight(IEnumerable<string> words)
        {
            var wordsWeight = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (wordsWeight.ContainsKey(word))
                {
                    wordsWeight[word]++;
                }
                else
                {
                    wordsWeight[word] = 0;
                }
            }

            return wordsWeight;
        }
    }
}