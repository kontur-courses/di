using System;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class FontSizeSettings : IFontSizeSettings
    {
        public float MaxFontSize { get; }
        public float MinFontSize { get; }

        public FontSizeSettings(float maxFontSize, float minFontSize)
        {
            MaxFontSize = ValidateSize(maxFontSize);
            MinFontSize = ValidateSize(minFontSize);
        }

        private float ValidateSize(float value)
        {
            if (value <= 0)
                throw fontSizeException;

            return value;
        }

        private readonly ApplicationException
            fontSizeException = new("Font size must be positive.");
    }
}