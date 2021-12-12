using System.Collections;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
    interface ITokenOrderer
    {
        IEnumerable<Token> OrderTokens(Token[] tokens);
    }
}