using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public class DefaultVisualizerSettings : IVisualizerSettings
    {
        public Color WordsColor => Color.White;
        public Color BackgroundColor => Color.Black;
        public Font Font => new Font("Arial", 50);
        public Size ImageSize => new Size(1920, 1080);
    }
}