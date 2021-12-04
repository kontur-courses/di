using System;
using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public interface IFontSizeSettings
    {
        public float MaxFontSize { get; set; }
        public float MinFontSize { get; set; }
    }

    public interface IFontFamilySettings
    {
        public FontFamily FontFamily { get; set; }
    }

    public interface IFontSettings : IFontSizeSettings, IFontFamilySettings { }

    public class DefaultFontSettings : IFontSettings
    {
        public FontFamily FontFamily { get; set; } = FontFamily.GenericMonospace;

        public float MaxFontSize
        {
            get => maxFontSize;
            set => maxFontSize = ValidateSize(value);
        }

        public float MinFontSize
        {
            get => minFontSize;
            set => minFontSize = ValidateSize(value);
        }

        private float maxFontSize = 32;
        private float minFontSize = 10;

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