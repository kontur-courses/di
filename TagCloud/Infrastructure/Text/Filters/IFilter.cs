using System.Collections.Generic;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Filters
{
    public interface IFilter<TToken>
    {
        public IEnumerable<(TToken token, TokenInfo info)> Filter(IEnumerable<(TToken token, TokenInfo info)> tokens);
    }
}