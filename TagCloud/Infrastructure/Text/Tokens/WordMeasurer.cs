using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Infrastructure.Settings;

namespace TagCloud.Infrastructure.Text.Tokens
{
    public class WordMeasurer : ITokenMeasurer<string>
    {
        private readonly Func<IFontSettingProvider> fontSettingProvider;

        public WordMeasurer(Func<IFontSettingProvider> fontSettingProvider)
        {
            this.fontSettingProvider = fontSettingProvider;
        }
        
        public Dictionary<string, Size> GetSizes(Dictionary<string, int> tokenFontSizes)
        {
            var result = new Dictionary<string, Size>();
            foreach (var word in tokenFontSizes.Keys)
            {
                var fontSize =  tokenFontSizes[word];
                var font = new Font(fontSettingProvider().FontFamily, fontSize);
                result[word] = TextRenderer.MeasureText(word, font);
            }

            return result;
        }
    }
}