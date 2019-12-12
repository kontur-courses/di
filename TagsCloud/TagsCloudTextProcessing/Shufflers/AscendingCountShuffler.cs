using System.Collections.Generic;
using System.Linq;

namespace TagsCloudTextProcessing.Shufflers
{
    public class AscendingCountShuffler: ITokenShuffler
    {
        public IEnumerable<Token> Shuffle(IEnumerable<Token> tokens)
        {
            return tokens.OrderBy(token => token.Count);
        }
    }
}