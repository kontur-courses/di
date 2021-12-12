using System.Collections;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITokenOrderer
    {
        IEnumerable<Token> OrderTokens(Token[] tokens);
    }
}