using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Text.Conveyors;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text
{
    public class WordAnalyzer : ITokenAnalyzer<string>
    {
        private readonly IEnumerable<IConveyor<string>> filters;

        public WordAnalyzer(IEnumerable<IConveyor<string>> filters)
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