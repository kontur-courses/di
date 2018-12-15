using System.Drawing;
using TagsCloudContainer.Filtering;
using TagsCloudContainer.Reading;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public class ApplicationSettings
    {
        public ReadingSettings ReadingSettings { get; }
        public FilterSettings FilterSettings { get; }
        public ImageSettings ImageSettings { get; }

        public Point TagsCloudCenter { get; }

        public ApplicationSettings
        (ReadingSettings readingSettings, FilterSettings filterSettings, Point tagsCloudCenter,
            ImageSettings imageSettings)
        {
            ReadingSettings = readingSettings;
            FilterSettings = filterSettings;
            TagsCloudCenter = tagsCloudCenter;
            ImageSettings = imageSettings;
        }
    }
}