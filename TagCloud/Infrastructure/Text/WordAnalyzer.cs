using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Text.Filters;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text
{
    public class WordAnalyzer : ITokenAnalyzer<string>
    {
        private readonly IEnumerable<IFilter<string>> filters;

        public WordAnalyzer(IEnumerable<IFilter<string>> filters)
        {
            this.filters = filters;
        }

        public IEnumerable<(string, TokenInfo)> Analyze(IEnumerable<string> words)
        {
            return filters.Aggregate(
                words.Select(line => (line, new TokenInfo())),
                (current, filter) => filter.Filter(current).ToArray());
        }
    }
}