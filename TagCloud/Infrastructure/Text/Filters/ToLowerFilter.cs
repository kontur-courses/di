using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Infrastructure.Text.Filters
{
    public class ToLowerFilter : IFilter<string>
    {
        public IEnumerable<string> Filter(IEnumerable<string> tokens)
        {
            return tokens.Select(token => token.ToLower());
        }
    }
}