using System.Collections.Generic;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text
{
    public interface ITokenAnalyzer<TToken>
    {
        public IEnumerable<(TToken, TokenInfo)> Analyze(IEnumerable<TToken> words);
    }
}