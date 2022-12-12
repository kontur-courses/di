using TagsCloudContainer.App.Layouter;

namespace TagsCloudContainer.Infrastructure
{
    public class ImageSettings
    {
        public PainterType PainterType { get; set; } = PainterType.OneColor;
        public int Width { get; set; } = 1200;
        public int Height { get; set; } = 700;
    }
}