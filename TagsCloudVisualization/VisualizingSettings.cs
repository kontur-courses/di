using System.Drawing;
using TagsCloudVisualization.PointDistributors;

namespace TagsCloudVisualization
{
    public class VisualizingSettings
    {
        public string ImageName = "TagCloud";
        public Size ImageSize = new Size(1000, 1000);
        public Color BackgroundColor = Color.Black;
        public Color PenColor = Color.DarkOrange;
        public FontFamily Font = new FontFamily("Arial");
        public IPointDistributor PointDistributor;
    
        public VisualizingSettings() { }

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
    }
}