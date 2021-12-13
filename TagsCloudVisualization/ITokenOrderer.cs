using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITokenOrderer
    {
        IEnumerable<Token> OrderTokens(IEnumerable<Token> tokens);
    }
}