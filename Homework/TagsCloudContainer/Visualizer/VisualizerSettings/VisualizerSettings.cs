using System.Drawing;
using TagsCloudContainer.Visualizer.ColorGenerators;

namespace TagsCloudContainer.Visualizer.VisualizerSettings
{
    public class VisualizerSettings : IVisualizerSettings
    {
        public VisualizerSettings(IColorGenerator wordsColorGenerator, Color backgroundColor, Font font, Size imageSize)
        {
            WordsColorGenerator = wordsColorGenerator;
            BackgroundColor = backgroundColor;
            Font = font;
            ImageSize = imageSize;
        }

        public IColorGenerator WordsColorGenerator { get; }
        public Color BackgroundColor { get; }
        public Font Font { get; }
        public Size ImageSize { get; }
    }
}