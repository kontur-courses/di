using System.Collections.Generic;

namespace TagCloud.Infrastructure.Text
{
    public interface IParser<in TIn, out TToken>
    {
        public IEnumerable<TToken> Parse(TIn text);
    }
}