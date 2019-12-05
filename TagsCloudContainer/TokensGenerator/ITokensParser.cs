using System.Collections.Generic;

namespace TagsCloudContainer.TokensGenerator
{
    public interface ITokensParser
    {
        IEnumerable<string> GetTokens(string str);
    }
}