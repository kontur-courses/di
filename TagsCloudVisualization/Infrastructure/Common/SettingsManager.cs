using TagsCloudLayouters.Configuration;

namespace TagsCloudVisualization.Infrastructure.Common
{
    public class SettingsManager
    {
        public ImageSettings ImageSettings { get; set;  }
        public CloudLayouterSettings LayouterSettings { get; set; }
        public TagsSettings TagsSettings { get; set; }
        public WordHandlerSettings WordHandlerSettings { get; set; }

        // public SettingsManager(
        //     ImageSettings imageSettings,
        //     CloudLayouterSettings layouterSettings, 
        //     WordHandlerSettings wordHandlerSettings)
        // {
        //     ImageSettings = imageSettings;
        //     LayouterSettings = layouterSettings;
        //     WordHandlerSettings = wordHandlerSettings;
        //     TagsSettings = new TagsSettings();
        // }
    }
}