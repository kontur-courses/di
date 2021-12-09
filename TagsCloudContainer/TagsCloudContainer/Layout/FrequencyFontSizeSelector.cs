using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Layout
{
    public class FrequencyFontSizeSelector : IFontSizeSelector
    {
        private readonly IFontSizeSettings fontSizeSettings;
        private readonly IWordsScaleSettings wordsScaleSettings;

        public FrequencyFontSizeSelector(IFontSizeSettings fontSizeSettings, IWordsScaleSettings wordsScaleSettings)
        {
            this.fontSizeSettings = fontSizeSettings;
            this.wordsScaleSettings = wordsScaleSettings;
        }

        public IEnumerable<FontSizedWord> GetFontSizedWords(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            var wordsFrequencies = words.GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count());
            var maxFrequency = wordsFrequencies.Values.Max();
            var minFrequency = wordsFrequencies.Values.Min();
            var firstPoint = new PointF(minFrequency, fontSizeSettings.MinFontSize);
            var secondPoint = new PointF(maxFrequency, fontSizeSettings.MaxFontSize);

            return wordsFrequencies.Select(
                pair =>
                    new FontSizedWord(
                        pair.Key,
                        wordsScaleSettings.Function.GetValue(firstPoint, secondPoint, pair.Value)));
        }
    }
}