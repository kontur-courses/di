using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Default
{
    public class TokenOrdererByDescendingWeight : ITokenOrderer
    {
        public IEnumerable<Token> OrderTokens(Token[] tokens)
        {
            return tokens.OrderByDescending(t => t.Weight);
        }
    }
}