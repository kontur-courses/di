using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Default
{
    public class TokenSortedOrder : ITokenOrderer
    {
        public IEnumerable<Token> OrderTokens(IEnumerable<Token> tokens)
        {
            return tokens.OrderByDescending(t => t.Weight).ThenByDescending(t => t.Value.Length);
        }
    }
}