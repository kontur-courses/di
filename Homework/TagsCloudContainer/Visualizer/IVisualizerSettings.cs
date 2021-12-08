using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public interface IVisualizerSettings
    {
        public Color WordsColor { get; }
        public Color BackgroundColor { get; }
        public Font Font { get; }
        public Size ImageSize { get; }
    }
}