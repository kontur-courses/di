using System.Collections;
using System.Collections.Generic;

namespace TagCloud.Infrastructure.Text.Tokens
{
    public interface ITokenCounter<TToken>
    {
        public Dictionary<TToken, int> GetFontSizes(IEnumerable<TToken> tokens);
    }
}