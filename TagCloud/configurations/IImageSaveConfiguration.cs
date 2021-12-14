using System.Drawing.Imaging;

namespace TagCloud.configurations
{
    public interface IImageSaveConfiguration
    {
        string? GetFilename();
        ImageFormat GetImageFormat();
    }
}