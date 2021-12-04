using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace TagCloudContainer.Infrastructure.Saver
{
    [SupportedOSPlatform("windows")]
    public class ImageSaver : IImageSaver
    {
        public static IReadOnlyDictionary<string, ImageFormat> ImageFormats = new Dictionary<string, ImageFormat>
        {
            ["png"] = ImageFormat.Png,
            ["bmp"] = ImageFormat.Bmp,
            ["jpg"] = ImageFormat.Jpeg,
            ["tiff"] = ImageFormat.Tiff,
            ["gif"] = ImageFormat.Gif,
            ["exif"] = ImageFormat.Exif
        };

        public void Save(Bitmap bitmap, string filePath, string format)
        {
            if (!ImageFormats.TryGetValue(format.ToLowerInvariant(), out var imageFormat))
                throw new ArgumentException($"Acceptable formats: {string.Join(',', ImageFormats.Keys)}, but was {format.ToLowerInvariant()}");
            
            bitmap.Save($"{filePath}.{format.ToLowerInvariant()}", imageFormat);
            bitmap.Dispose();
        }
    }
}
