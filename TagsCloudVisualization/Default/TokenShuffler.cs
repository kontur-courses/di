using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Default
{
    public class TokenShuffler : ITokenOrderer
    {
        public IEnumerable<Token> OrderTokens(IEnumerable<Token> tokens)
        {
            var rnd = new Random();
            return tokens.OrderBy(x => rnd.Next());
        }
    }
}