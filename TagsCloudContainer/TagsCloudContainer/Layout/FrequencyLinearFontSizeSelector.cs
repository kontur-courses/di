using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Layout
{
    public class FontSizeRange
    {
        public float MaxFontSize { get; }

        public float MinFontSize { get; }

        public FontSizeRange(int maxFontSize, int minFontSize)
        {
            if (maxFontSize <= 0 || minFontSize <= 0)
                throw new ArgumentException("Font size must be positive.");

            MaxFontSize = maxFontSize;
            MinFontSize = minFontSize;
        }
    }

    public interface IFontSizeSelector
    {
        IEnumerable<(string Word, float FontSize)> GetFontSizes(IEnumerable<string> words);
    }

    public class FrequencyLinearFontSizeSelector : IFontSizeSelector
    {
        private readonly FontSizeRange fontSizeRange;

        public FrequencyLinearFontSizeSelector(FontSizeRange fontSizeRange)
        {
            this.fontSizeRange = fontSizeRange;
        }

        public IEnumerable<(string Word, float FontSize)> GetFontSizes(IEnumerable<string> words)
        {
            var wordsFrequencies = new FrequencyDictionary<string>(words);
            var maxFrequency = wordsFrequencies.Values.Max();
            var minFrequency = wordsFrequencies.Values.Min();

            var (k, b) = GetLineCoefficients(maxFrequency, minFrequency);

            foreach (var (word, frequency) in wordsFrequencies)
                yield return (word, GetFunctionValue(k, b, frequency));
        }

        private (float K, float B) GetLineCoefficients(int maxFrequency, int minFrequency)
        {
            var k = (fontSizeRange.MaxFontSize - fontSizeRange.MinFontSize) / (maxFrequency - minFrequency);
            var b = fontSizeRange.MinFontSize - k * minFrequency;
            return (k, b);
        }

        private float GetFunctionValue(float k, float b, int x)
        {
            var value = k * x + b;
            if (float.IsNaN(value))
                return (fontSizeRange.MinFontSize + fontSizeRange.MaxFontSize) / 2;

            return value;
        }
    }
}