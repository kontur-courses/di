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

        public IEnumerable<(string token, TokenInfo info)> Handle(IEnumerable<(string token, TokenInfo info)> tokens)
        {
            var baseFont = fontSettingProvider();
            var tokensWithInfo = tokens.ToList();
            var maxCount = tokensWithInfo.Select(pair => pair.info.Frequency).Aggregate(Math.Max);
            var minCount = tokensWithInfo.Select(pair => pair.info.Frequency).Aggregate(Math.Min);
            var currentGradient = Math.Max(baseFont.MaxFontSize - baseFont.MinFontSize, 1);
            var desiredGradient = Math.Max(maxCount - minCount, 1);

            int FontSizeLine(int x)
            {
                return (x - minCount) * currentGradient / desiredGradient + baseFont.MinFontSize;
            }

            foreach (var (word, info) in tokensWithInfo)
            {
                var count = info.Frequency;
                info.FontSize = FontSizeLine(count);
                yield return (word, info);
            }
        }
    }
}