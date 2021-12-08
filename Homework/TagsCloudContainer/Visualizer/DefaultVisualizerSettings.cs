using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public class DefaultVisualizerSettings : IVisualizerSettings
    {
        public Color WordsColor => Color.White;
        public Color BackgroundColor => Color.Black;
        public Font Font => new("Arial", 50);
        public Size ImageSize => new(1920, 1080);
    }
}