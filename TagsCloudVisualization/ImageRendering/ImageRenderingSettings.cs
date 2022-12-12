using System.Drawing;

namespace TagsCloudVisualization.ImageRendering
{
    internal class ImageRenderingSettings : IImageRenderingSettings
    {
        public int Height { get; }
        public int Width { get; }

        public ImageRenderingSettings(int height, int width)
        {
            Height = height;
            Width = width;
        }
    }
}
