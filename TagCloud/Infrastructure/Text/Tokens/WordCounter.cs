using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Infrastructure.Settings;

namespace TagCloud.Infrastructure.Text.Tokens
{
    public class WordCounter : ITokenCounter<string>
    {
        private readonly Func<IFontSettingProvider> fontSettingProvider;
        private readonly Func<IWordCountThresholdSettingProvider> wordCountThresholdProvider;

        public WordCounter(Func<IFontSettingProvider> fontSettingProvider, Func<IWordCountThresholdSettingProvider> wordCountThresholdProvider)
        {
            this.fontSettingProvider = fontSettingProvider;
            this.wordCountThresholdProvider = wordCountThresholdProvider;
        }
        
        public Dictionary<string, int> GetFontSizes(IEnumerable<string> tokens)
        {
            var wordCount = GetCount(tokens);
            var baseFont = fontSettingProvider();
            var maxCount = wordCount.Aggregate((x, y) => x.Value > y.Value ? x : y).Value;
            var minCount = wordCount.Aggregate((x, y) => x.Value < y.Value ? x : y).Value;
            
            int FontSizeLine(int x) => (x - minCount) 
                                       * (baseFont.MaxFontSize - baseFont.MinFontSize) 
                                       / (maxCount - minCount) 
                                       + baseFont.MinFontSize; 
            var result = new Dictionary<string, int>();
            var threshold = wordCountThresholdProvider().WordCountThreshold;
            foreach (var word in wordCount.Keys)
            {
                var count =  wordCount[word];
                if (count < threshold)
                    continue;
                var fontSize = FontSizeLine(count);
                result[word] = fontSize;
            }

            return result;
        }

        private Dictionary<string, int> GetCount(IEnumerable<string> tokens)
        {
            var count = new Dictionary<string, int>();
            tokens = tokens.ToArray();
            foreach (var token in tokens)
            {
                if (count.ContainsKey(token))
                    count[token]++;
                else
                    count.Add(token, 1);
            }

            return count;
        }
    }
}