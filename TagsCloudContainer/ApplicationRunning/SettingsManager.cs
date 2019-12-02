using System;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.CloudVisualizers.ImageSaving;
using TagsCloudContainer.TextParsing.CloudParsing;

namespace TagsCloudContainer.ApplicationRunning
{
    public class SettingsManager
    {
        public CloudLayouterSettings GetLayouterSettings()
        {
            return new CloudLayouterSettings();
        }

        public CloudVisualizerSettings GetVisualizerSettings()
        {
            return new CloudVisualizerSettings();
        }

        public ImageSaverSettings GetImageSaverSettings()
        {
            return new ImageSaverSettings();
        }
        
        public CloudWordsParserSettings GetWordsParserSettings()
        {
            return new CloudWordsParserSettings();
        }
    }
}