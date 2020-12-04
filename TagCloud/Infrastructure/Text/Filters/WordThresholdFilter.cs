using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Filters
{
    public class WordThresholdFilter : IFilter<string>
    {
        private readonly Func<IWordCountThresholdSettingProvider> wordCountThresholdProvider;

        public WordThresholdFilter(Func<IWordCountThresholdSettingProvider> wordCountThresholdProvider)
        {
            this.wordCountThresholdProvider = wordCountThresholdProvider;
        }

        public IEnumerable<(string token, TokenInfo info)> Filter(IEnumerable<(string token, TokenInfo info)> tokens)
        {
            var threshold = wordCountThresholdProvider().WordCountThreshold;
            return tokens.Where(pair => pair.info.Frequency > threshold);
        }
    }
}