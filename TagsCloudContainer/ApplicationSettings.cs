using System.Drawing;
using TagsCloudContainer.Filtering;
using TagsCloudContainer.Reading;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public class ApplicationSettings
    {
        public ImageSettings ImageSettings { get; }
        public string InputPath { get; }
        public string BlackListPath { get; }
        public Point TagsCloudCenter { get; }

        public ApplicationSettings
            (string inputPath, string blackListPath, Point tagsCloudCenter, ImageSettings imageSettings)
        {
            TagsCloudCenter = tagsCloudCenter;
            ImageSettings = imageSettings;
            InputPath = inputPath;
            BlackListPath = blackListPath;
        }
    }
}