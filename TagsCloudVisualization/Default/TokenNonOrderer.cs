using System.Collections.Generic;

namespace TagsCloudVisualization.Default
{
    public class TokenNonOrderer : ITokenOrderer
    {
        public IEnumerable<Token> OrderTokens(IEnumerable<Token> tokens)
        {
            return tokens;
        }
    }
}