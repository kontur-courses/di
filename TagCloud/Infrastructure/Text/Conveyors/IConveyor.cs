using System.Collections.Generic;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Conveyors
{
    public interface IConveyor
    {
        public IEnumerable<(string token, TokenInfo info)> Handle(IEnumerable<(string token, TokenInfo info)> tokens);
    }
}