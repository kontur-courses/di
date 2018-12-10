using TagsCloudContainer.ImageCreators;

namespace TagsCloudContainer.Settings
{
    public class SettingsManager
    {
        public readonly ImageSettings ImageSettings;
        public readonly TextSettings TextSettings;
        public readonly Palette Palette;

        public SettingsManager(ImageSettings imageSettings, TextSettings textSettings, Palette palette)
        {
            ImageSettings = imageSettings;
            TextSettings = textSettings;
            Palette = palette;
        }
    }
}