using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public class VisualizerSettings : IVisualizerSettings
    {
        public Color WordsColor { get; }
        public Color BackgroundColor { get; }
        public Font Font { get; }
        public Size ImageSize { get; }
    }
}