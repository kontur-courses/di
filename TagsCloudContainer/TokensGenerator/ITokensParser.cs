using System.Collections.Generic;

namespace TagsCloudContainer.TokensGenerator
{
    public interface ITokensParser
    {
        IEnumerable<Token> GetTokens(string str);
    }
}