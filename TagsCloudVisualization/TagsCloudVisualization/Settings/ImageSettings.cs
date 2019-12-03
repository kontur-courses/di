using System.Drawing;


namespace TagsCloudVisualization.Settings
{
    public class ImageSettings
    {
        public Size ImageSize { get; private set; }
        public string ImageName { get; private set; }
        public string ImageExtention { get; private set; }
        public int MinimalTextSize { get; private set; }

        public ImageSettings(Size imageSize, string imageName, string imageExtention, int minimalTextSize)
        {
            ImageSize = imageSize;
            ImageName = imageName;
            ImageExtention = imageExtention;
            MinimalTextSize = minimalTextSize;
        }
    }
}
