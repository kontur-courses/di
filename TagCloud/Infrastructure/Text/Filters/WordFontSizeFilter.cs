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
            var tg1 = Math.Max(baseFont.MaxFontSize - baseFont.MinFontSize, 1);
            var tg2 = Math.Max(maxCount - minCount, 1);
            
            int FontSizeLine(int x)
            {
                return (x - minCount) * tg1 / tg2 + baseFont.MinFontSize;
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