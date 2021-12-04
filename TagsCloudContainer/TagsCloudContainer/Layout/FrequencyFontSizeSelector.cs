using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Layout
{
    public interface IFontSizeSelector
    {
        IEnumerable<(string Word, float FontSize)> GetFontSizes(IEnumerable<string> words);
    }

    public class FrequencyFontSizeSelector : IFontSizeSelector
    {
        private readonly IFontSizeSettings fontSizeSettings;
        private readonly IWordsScaleSettings wordsScaleSettings;

        public FrequencyFontSizeSelector(IFontSizeSettings fontSizeSettings, IWordsScaleSettings wordsScaleSettings)
        {
            this.fontSizeSettings = fontSizeSettings ?? throw new ArgumentNullException(nameof(fontSizeSettings));
            this.wordsScaleSettings = wordsScaleSettings ?? throw new ArgumentNullException(nameof(wordsScaleSettings));
        }

        public IEnumerable<(string Word, float FontSize)> GetFontSizes(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            var wordsFrequencies = new FrequencyDictionary<string>(words);
            var maxFrequency = wordsFrequencies.Values.Max();
            var minFrequency = wordsFrequencies.Values.Min();
            var scaler = wordsScaleSettings.GetScaler(new PointF(minFrequency, fontSizeSettings.MinFontSize),
                new PointF(maxFrequency, fontSizeSettings.MaxFontSize));

            foreach (var (word, frequency) in wordsFrequencies)
                yield return (word, scaler.GetValue(frequency));
        }
    }
}