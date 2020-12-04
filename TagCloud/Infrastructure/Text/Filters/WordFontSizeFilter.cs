using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Filters
{
    public class WordFontSizeFilter : IFilter<string>
    {
        private readonly Func<IFontSettingProvider> fontSettingProvider;

        public WordFontSizeFilter(Func<IFontSettingProvider> fontSettingProvider)
        {
            this.fontSettingProvider = fontSettingProvider;
        }

        public IEnumerable<(string token, TokenInfo info)> Filter(IEnumerable<(string token, TokenInfo info)> tokens)
        {
            var baseFont = fontSettingProvider();
            var maxCount = tokens.Select(pair => pair.info.Frequency).Aggregate(Math.Max);
            var minCount = tokens.Select(pair => pair.info.Frequency).Aggregate(Math.Min);

            int FontSizeLine(int x)
            {
                return (x - minCount)
                       * (baseFont.MaxFontSize - baseFont.MinFontSize)
                       / (maxCount - minCount)
                       + baseFont.MinFontSize;
            }

            foreach (var (word, info) in tokens)
            {
                var count = info.Frequency;
                info.FontSize = FontSizeLine(count);
                yield return (word, info);
            }
        }
    }
}