using FluentResults;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.Infrastructure.WordColorProviders
{
    public class WordLinearColorProvider : IWordColorProvider
    {
        private readonly WordColorSettings settings;

        private readonly int minFrequency;
        private readonly int maxFrequency;
        private readonly float frequencyDelta;

        public WordLinearColorProvider(WordColorSettings settings) 
        {
            this.settings = settings;

            var frequencies = settings.WordFrequencies.Values;
            minFrequency = frequencies.Min();
            maxFrequency = frequencies.Max();
            frequencyDelta = maxFrequency - minFrequency;
        }

        public Result<Color> GetColor(string word)
        {
            return Result.OkIf(settings.WordFrequencies.ContainsKey(word), $"Can't generate color for word '{word}'")
                         .Bind(() => Result.Ok(GenerateColor(GetNormalizedFrequency(settings.WordFrequencies[word]))));
        }

        private float GetNormalizedFrequency(int frequency)
        {
            return (frequency - minFrequency) / frequencyDelta;
        }

        private Color GenerateColor(float normalizedFrequency)
        {
            var startColor = settings.MinFrequencyColor;
            var endColor = settings.MaxFrequencyColor;

            return Color.FromArgb(GenerateColorComponent(startColor.A, endColor.A, normalizedFrequency),
                                  GenerateColorComponent(startColor.R, endColor.R, normalizedFrequency),
                                  GenerateColorComponent(startColor.G, endColor.G, normalizedFrequency),
                                  GenerateColorComponent(startColor.B, endColor.B, normalizedFrequency));
        }

        private static int GenerateColorComponent(int start, int end, float step)
        {
            return start + (int)Math.Round(step * (end - start));
        }
    }
}