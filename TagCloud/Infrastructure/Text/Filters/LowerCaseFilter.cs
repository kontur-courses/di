using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Filters
{
    public class LowerCaseFilter : IFilter<string>
    {
        public IEnumerable<(string token, TokenInfo info)> Filter(IEnumerable<(string token, TokenInfo info)> tokens)
        {
            return tokens.Select(pair => (pair.token.ToLower(), pair.info));
        }
    }
}