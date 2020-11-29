using System.Collections.Generic;

namespace TagCloud.Infrastructure.Text
{
    public interface IParser<out TToken>
    {
        public IEnumerable<TToken> Parse();
    }
}