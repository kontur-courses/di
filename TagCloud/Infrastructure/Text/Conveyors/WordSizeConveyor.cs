using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Conveyors
{
    public class WordSizeConveyor : IConveyor<string>
    {
        private readonly Func<IFontSettingProvider> fontSettingProvider;

        public WordSizeConveyor(Func<IFontSettingProvider> fontSettingProvider)
        {
            this.fontSettingProvider = fontSettingProvider;
        }

        public IEnumerable<(string token, TokenInfo info)> Handle(IEnumerable<(string token, TokenInfo info)> tokens)
        {
            var result = new Dictionary<string, TokenInfo>();
            foreach (var (word, info) in tokens)
            {
                var fontSize = info.FontSize;
                var font = new Font(fontSettingProvider().FontFamily, fontSize);
                info.Size = TextRenderer.MeasureText(word, font);
                result[word] = info;
            }

            return result.Select(pair => (pair.Key, pair.Value));
        }
    }
}