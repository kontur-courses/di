using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Filters;

namespace TagsCloudContainer.TokensGenerator
{
    public class TokensParser : ITokensParser
    {
        private Dictionary<string, int> CountToken = new Dictionary<string, int>();
        private IFilter Filter { get; }

        public TokensParser(IFilter filter)
        {
            Filter = filter;
        }

        public IEnumerable<Token> GetTokens(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }
            var splitToken = str.Split(new[] {"\r\n","\r"}, StringSplitOptions.RemoveEmptyEntries);
            var filterToken = Filter.Filtering(splitToken);
            foreach (var token in filterToken)
            {
                if (CountToken.ContainsKey(token))
                {
                    CountToken[token]++;
                }
                else
                {
                    CountToken[token] = 1;
                }
            }
            return CountToken.Select(token => new Token(token.Key, (uint) token.Value));
        }
    }
}