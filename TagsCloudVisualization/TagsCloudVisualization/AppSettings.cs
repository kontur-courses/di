namespace TagsCloudVisualization
{
    public class AppSettings : ImageSettingsProvider
    {
        public AppSettings()
        {
            ImageSettings = ImageSettings.InitializeDefaultSettings();
        }

        public ImageSettings ImageSettings { get; set; }
    }
}