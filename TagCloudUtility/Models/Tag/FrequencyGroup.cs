using System;

namespace TagCloud.Utility.Models.Tag
{
    public struct FrequencyGroup
    {
        public double MinFrequencyCoef { get; }
        public double MaxFrequencyCoef { get; }

        public FrequencyGroup(double minFrequencyCoef, double maxFrequencyCoef)
        {
            if (minFrequencyCoef < 0
                || minFrequencyCoef > 1
                || maxFrequencyCoef < 0
                || maxFrequencyCoef > 1
                || minFrequencyCoef > maxFrequencyCoef)
                throw new ArgumentException($"Min and Max should be in 0..1 and min <= max but was {minFrequencyCoef} - {maxFrequencyCoef}");
            MinFrequencyCoef = minFrequencyCoef;
            MaxFrequencyCoef = maxFrequencyCoef;
        }

        public bool IntersectWith(FrequencyGroup other)
        {
            return MinFrequencyCoef >= other.MinFrequencyCoef && MinFrequencyCoef < other.MaxFrequencyCoef
                   || MaxFrequencyCoef > other.MinFrequencyCoef && MaxFrequencyCoef <= other.MaxFrequencyCoef;
        }

        public bool Contains(double frequencyCoef)
        {
            return MinFrequencyCoef <= frequencyCoef && MaxFrequencyCoef >= frequencyCoef;
        }
    }
}