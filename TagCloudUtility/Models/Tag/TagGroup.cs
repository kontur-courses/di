using System;

namespace TagCloud.Utility.Models.Tag
{
    public struct TagGroup : ITagGroup
    {
        public int FontSize { get; }
        public FrequencyGroup FrequencyGroup { get; }

        public TagGroup(int fontSize, FrequencyGroup frequencyGroup)
        {
            if (fontSize <= 0)
                throw new ArgumentException($"Font size can't be negative or zero, but was {fontSize}");
            FontSize = fontSize;
            FrequencyGroup = frequencyGroup;
        }

        public bool Contains(double frequencyCoef)
        {
            return FrequencyGroup.Contains(frequencyCoef);
        }
    }
}