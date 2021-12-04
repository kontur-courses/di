using System.Drawing;

namespace TagsCloudVisualization.ImageCreator
{
    public interface IImageSettingsProvider
    {
        public Color BackgroundColor { get; init; }
        public Size ImageSize { get; init; }
    }
}