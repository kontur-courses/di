using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Configuration;

namespace TagsCloudContainer.Visualizer
{
    public class VisualizerSettings : IVisualizerSettings
    {
        public Color Color { get; }
        public int ImageWidth { get; }
        public int ImageHeight { get; }
        public ImageFormat ImageFormat { get; }

        public VisualizerSettings(IConfiguration configuration)
        {
            Color = configuration.Color;
            ImageWidth = configuration.ImageWidth;
            ImageHeight = configuration.ImageHeight;
            ImageFormat = configuration.ImageFormat;
        }
    }
}