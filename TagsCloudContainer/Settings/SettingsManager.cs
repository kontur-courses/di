using TagsCloudContainer.ImageCreators;

namespace TagsCloudContainer.Settings
{
    public class SettingsManager: ISettingsManager
    {
        public IImageSettings ImageSettings { get; }
        public ITextSettings TextSettings { get; }
        public IPalette Palette { get; }

        public SettingsManager(IImageSettings imageSettings, ITextSettings textSettings, IPalette palette)
        {
            ImageSettings = imageSettings;
            TextSettings = textSettings;
            Palette = palette;
        }
    }
}