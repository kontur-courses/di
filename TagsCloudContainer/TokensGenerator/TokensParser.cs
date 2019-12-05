using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Filters;

namespace TagsCloudContainer.TokensGenerator
{
    public class TokensParser : ITokensParser
    {

        public TokensParser()
        {
        }

        public IEnumerable<string> GetTokens(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }
            var splitToken = str.Split(new[] {"\r\n","\r"}, StringSplitOptions.RemoveEmptyEntries);
            return splitToken;
        }
    }
}