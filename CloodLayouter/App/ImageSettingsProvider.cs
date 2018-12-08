using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class ImageSettingsProvider : IImageSettingsProvider
    {
        public int Width { get; set; } = 500;
        public int Height { get; set; } = 500;
    }
}