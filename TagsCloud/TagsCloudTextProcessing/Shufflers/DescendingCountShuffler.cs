using System.Collections.Generic;
using System.Linq;

namespace TagsCloudTextProcessing.Shufflers
{
    public class DescendingCountShuffler : ITokenShuffler
    {
        public IEnumerable<Token> Shuffle(IEnumerable<Token> tokens)
        {
            return tokens.OrderByDescending(token => token.Count);
        }
    }
}