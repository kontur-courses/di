using System.Collections.Generic;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Conveyors
{
    public interface IConveyor<TToken>
    {
        public IEnumerable<(TToken token, TokenInfo info)> Handle(IEnumerable<(TToken token, TokenInfo info)> tokens);
    }
}