using System.Drawing;
using TagCloudContainer.Interfaces;

namespace TagCloudContainer.Models
{
    public class FontSettings : IFontSettings
    {
        private int maxFontSize = 72;
        private int minFontSize = 32;

        public int MaxFontSize 
        { 
            get => maxFontSize; 
            set 
            {
                if (!IsFontSizesCorrect(minFontSize, value))
                    throw new ArgumentException("Incorrect font size");

                maxFontSize = value;
            }
        }

        public int MinFontSize
        {
            get => minFontSize;
            set
            {
                if (!IsFontSizesCorrect(value, maxFontSize))
                    throw new ArgumentException("Incorrect font size");

                minFontSize = value;
            }
        }

        public FontFamily Font { get; set; } = new FontFamily("Arial");

        public bool IsFontSizesCorrect(int min, int max)
            => max > 0 && min > 0 && max > min;
    }
}
