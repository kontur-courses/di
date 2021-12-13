using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Default
{
    public class WordCounter : ITokenWeigher
    {
        public Token[] Evaluate(IEnumerable<string> words, int maxTokenCount)
        {
            var wordsCount = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!wordsCount.ContainsKey(word))
                    wordsCount[word] = 1;
                else 
                    wordsCount[word] += 1;
            }
            return wordsCount.Select(kvp => new Token(kvp.Key, kvp.Value))
                .OrderByDescending(t => t.Weight)
                .Take(maxTokenCount)
                .ToArray();
        }
    }
}