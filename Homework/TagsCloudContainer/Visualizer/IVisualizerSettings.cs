using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public interface IVisualizerSettings
    {
        public Color WordsColor { get; set; }
        public Color BackgroundColor { get; set; }
        public Font Font { get; set; }
        public Size ImageSize { get; set; }
    }
}