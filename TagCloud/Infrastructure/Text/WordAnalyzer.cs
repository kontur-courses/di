using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Text.Filters;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text
{
    public class WordAnalyzer<TToken>
    {
        private readonly IEnumerable<IFilter<TToken>> filters;

        public WordAnalyzer(IEnumerable<IFilter<TToken>> filters)
        {
            this.filters = filters;
        }

        public IEnumerable<(TToken, TokenInfo)> Analyze(IEnumerable<TToken> words)
        {
            return filters.Aggregate(
                words.Select(line => (line, new TokenInfo())),
                (current, filter) => filter.Filter(current).ToArray());
        }
    }
}