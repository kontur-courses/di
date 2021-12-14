using System.Drawing.Imaging;

namespace TagCloud.configurations
{
    public class ImageSaveConfiguration : IImageSaveConfiguration
    {
        private readonly string? filename;
        private readonly ImageFormat format;

        public ImageSaveConfiguration(string? filename, ImageFormat format)
        {
            this.filename = filename;
            this.format = format;
        }

        public string? GetFilename() => filename;

        public ImageFormat GetImageFormat() => format;
    }
}