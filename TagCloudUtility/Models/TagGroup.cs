using System;
using System.Drawing;

namespace TagCloud.Utility.Models
{
    public class TagGroup
    {
        public Size Size { get; }
        public FrequencyGroup FrequencyGroup { get; }

        public TagGroup(Size size, FrequencyGroup frequencyGroup)
        {
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException($"Size can't be negative or zero");
            Size = size;
            FrequencyGroup = frequencyGroup;
        }

        public bool Contains(double frequencyCoef)
        {
            return FrequencyGroup.Contains(frequencyCoef);
        }

        public Size GetSizeForWord(string word)
        {
            return new Size(word.Length * Size.Width, Size.Height);
        }
    }
}