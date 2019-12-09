using System.Collections.Generic;
using System.Linq;

namespace TagsCloudTextPreparation.Shufflers
{
    public class TokenShufflerAscending: ITokenShuffler
    {
        public IEnumerable<Token> Shuffle(IEnumerable<Token> tokens)
        {
            return tokens.OrderBy(token => token.Count);
        }
    }
}