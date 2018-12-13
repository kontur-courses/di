using TagsCloudContainer.Configuration;

namespace TagsCloudContainer.Visualizer
{
    public class VisualizerSettings : IVisualizerSettings
    {
        public string Color { get; }
        public int ImageWidth { get; }
        public int ImageHeight { get; }

        public VisualizerSettings(IConfiguration configuration)
        {
            Color = configuration.Color;
            ImageWidth = configuration.ImageWidth;
            ImageHeight = configuration.ImageHeight;
        }
    }
}