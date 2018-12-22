using System.Collections.Generic;
using TagsCloudContainer.GettingTokens;

namespace TagsCloudContainer.Generation
{
    public interface ITokenizer
    {
        IEnumerable<Token> GetTokens(string text);
    }
}