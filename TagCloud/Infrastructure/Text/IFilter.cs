using System.Collections.Generic;

namespace TagCloud.Infrastructure.Text
{
    public interface IFilter<TToken>
    {
        public IEnumerable<TToken> Filter(IEnumerable<TToken> tokens);
    }
}