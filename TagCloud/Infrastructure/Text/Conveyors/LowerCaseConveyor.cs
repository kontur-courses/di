using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Conveyors
{
    public class LowerCaseConveyor : IConveyor<string>
    {
        public IEnumerable<(string token, TokenInfo info)> Handle(IEnumerable<(string token, TokenInfo info)> tokens)
        {
            return tokens.Select(pair => (pair.token.ToLower(), pair.info));
        }
    }
}