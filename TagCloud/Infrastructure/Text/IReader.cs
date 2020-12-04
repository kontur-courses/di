using System.Collections.Generic;

namespace TagCloud.Infrastructure.Text
{
    public interface IReader<TToken>
    {
        public IEnumerable<TToken> ReadTokens();
    }
}