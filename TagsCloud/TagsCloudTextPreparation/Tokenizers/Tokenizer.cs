using System.Collections.Generic;
using System.Linq;

namespace TagsCloudTextPreparation.Tokenizers
{
    public class Tokenizer : ITokenizer
    {
        public IEnumerable<Token> Tokenize(IEnumerable<string> words)
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