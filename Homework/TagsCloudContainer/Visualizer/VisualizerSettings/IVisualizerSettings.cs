using System.Drawing;
using TagsCloudContainer.Visualizer.ColorGenerators;

namespace TagsCloudContainer.Visualizer.VisualizerSettings
{
    public interface IVisualizerSettings
    {
        public IColorGenerator WordsColorGenerator { get; }
        public Color BackgroundColor { get; }
        public Font Font { get; }
        public Size ImageSize { get; }
    }
}