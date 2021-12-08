using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class FontSizeSettings : IFontSizeSettings
    {
        public float MaxFontSize { get; }
        public float MinFontSize { get; }

        public FontSizeSettings(float maxFontSize, float minFontSize)
        {
            MaxFontSize = Validate.Positive("Max font", maxFontSize);
            MinFontSize = Validate.Positive("Min font", minFontSize);
        }
    }
}