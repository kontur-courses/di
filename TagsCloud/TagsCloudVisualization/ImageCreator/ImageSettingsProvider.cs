using System.Drawing;

namespace TagsCloudVisualization.ImageCreator
{
    public class ImageSettingsProvider : IImageSettingsProvider
    {
        public Color BackgroundColor { get; init; } = Color.Gray;
        public Size ImageSize { get; init; } = new(800, 600);
    }
}