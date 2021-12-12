namespace TagsCloudContainer.Settings
{
    public class FontSizeSettings : IFontSizeSettings
    {
        public float MaxFontSize { get; }
        public float MinFontSize { get; }

        public FontSizeSettings(IRenderSettings settings)
        {
            MaxFontSize = Validate.Positive("Max font", settings.MaxFontSize);
            MinFontSize = Validate.Positive("Min font", settings.MinFontSize);
        }
    }
}