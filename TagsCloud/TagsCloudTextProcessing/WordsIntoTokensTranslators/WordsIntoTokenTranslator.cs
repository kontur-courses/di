using System.Collections.Generic;
using System.Linq;

namespace TagsCloudTextProcessing.WordsIntoTokensTranslators
{
    public class WordsIntoTokenTranslator : IWordsIntoTokenTranslator
    {
        public IEnumerable<Token> TranslateIntoTokens(IEnumerable<string> words)
        {
            var tokensDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (tokensDictionary.ContainsKey(word))
                    tokensDictionary[word]++;
                else
                    tokensDictionary.Add(word, 1);
            }

            return tokensDictionary.Select(v => new Token(v.Key, v.Value)).ToList();
        }
    }
}