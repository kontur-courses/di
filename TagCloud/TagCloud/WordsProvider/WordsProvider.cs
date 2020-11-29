using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    public abstract class WordsProvider : IWordsProvider
    {
        protected IEnumerable<WordToken> GetTokens(IEnumerable<string> words)
        {
            var frequencies = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencies.ContainsKey(word))
                    frequencies[word] = 0;
                frequencies[word]++;
            }

            return frequencies.Select(x => new WordToken(x.Key, x.Value));
        }

        public abstract IEnumerable<WordToken> GetTokens();
    }
}