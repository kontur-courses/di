using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Layout
{
    public class FrequencyFontSizeSelector : IFontSizeSelector
    {
        private readonly IFontSizeSettings fontSizeSettings;
        private readonly IScalersFactory scalersFactory;

        public FrequencyFontSizeSelector(IFontSizeSettings fontSizeSettings, IScalersFactory scalersFactory)
        {
            this.fontSizeSettings = fontSizeSettings;
            this.scalersFactory = scalersFactory;
        }

        public IEnumerable<FontSizedWord> GetFontSizes(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            var wordsFrequencies = words.GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count());
            var maxFrequency = wordsFrequencies.Values.Max();
            var minFrequency = wordsFrequencies.Values.Min();
            var scaler = scalersFactory.GetScaler(new PointF(minFrequency, fontSizeSettings.MinFontSize),
                new PointF(maxFrequency, fontSizeSettings.MaxFontSize));

            foreach (var (word, frequency) in wordsFrequencies)
                yield return new FontSizedWord(word, scaler.GetValue(frequency));
        }
    }
}