using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Default
{
    public class WordCounter : ITokenWeigher
    {
        public Token[] Evaluate(IEnumerable<string> words, int maxTokenCount)
        {
            return words.GroupBy(x => x).Select(word => new Token(word.Key, word.Count()))
                .OrderByDescending(t => t.Weight)
                .Take(maxTokenCount)
                .ToArray();
        }
    }
}