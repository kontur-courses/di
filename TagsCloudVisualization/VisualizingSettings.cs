using System.Drawing;

namespace TagsCloudVisualization
{
    public class VisualizingSettings
    {
        public VisualizingSettings(string imageName, Size imageSize)
        {
            ImageName = imageName;
            ImageSize = imageSize;
            BackgroundColor = Color.Black;
            PenColor = Color.DarkOrange;
        }

        public VisualizingSettings(string imageName, Size imageSize, Color backgroundColor, Color penColor)
        {
            ImageName = imageName;
            ImageSize = imageSize;
            BackgroundColor = backgroundColor;
            PenColor = penColor;
        }

        public readonly string ImageName;
        public readonly Size ImageSize;
        public readonly Color BackgroundColor;
        public readonly Color PenColor;
    }
}
