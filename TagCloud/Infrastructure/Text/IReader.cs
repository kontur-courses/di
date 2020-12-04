using System.Collections.Generic;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text
{
    public interface IReader<TToken>
    {
        public IEnumerable<TToken> ReadTokens();
    }
}