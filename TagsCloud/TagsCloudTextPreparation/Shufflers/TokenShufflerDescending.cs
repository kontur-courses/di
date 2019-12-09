using System.Collections.Generic;
using System.Linq;

namespace TagsCloudTextPreparation.Shufflers
{
    public class TokenShufflerDescending : ITokenShuffler
    {
        public IEnumerable<Token> Shuffle(IEnumerable<Token> tokens)
        {
            return tokens.OrderByDescending(token => token.Count);
        }
    }
}