using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Conveyors
{
    public class WordFontSizeConveyor : IConveyor<string>
    {
        private readonly Func<IFontSettingProvider> fontSettingProvider;

        public WordFontSizeConveyor(Func<IFontSettingProvider> fontSettingProvider)
        {
            this.fontSettingProvider = fontSettingProvider;
        }

        public IEnumerable<(string token, TokenInfo info)> Filter(IEnumerable<(string token, TokenInfo info)> tokens)
        {
            var baseFont = fontSettingProvider();
            var valueTuples = tokens.ToList();
            var maxCount = valueTuples.Select(pair => pair.info.Frequency).Aggregate(Math.Max);
            var minCount = valueTuples.Select(pair => pair.info.Frequency).Aggregate(Math.Min);
            var tg1 = Math.Max(baseFont.MaxFontSize - baseFont.MinFontSize, 1);
            var tg2 = Math.Max(maxCount - minCount, 1);

            int FontSizeLine(int x)
            {
                return (x - minCount) * tg1 / tg2 + baseFont.MinFontSize;
            }

            foreach (var (word, info) in valueTuples)
            {
                var count = info.Frequency;
                info.FontSize = FontSizeLine(count);
                yield return (word, info);
            }
        }
    }
}