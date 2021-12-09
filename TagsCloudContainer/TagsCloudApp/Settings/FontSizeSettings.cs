using TagsCloudApp.RenderCommand;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class FontSizeSettings : IFontSizeSettings
    {
        public float MaxFontSize { get; }
        public float MinFontSize { get; }

        public FontSizeSettings(IRenderOptions renderOptions)
        {
            MaxFontSize = Validate.Positive("Max font", renderOptions.MaxFontSize);
            MinFontSize = Validate.Positive("Min font", renderOptions.MinFontSize);
        }
    }
}