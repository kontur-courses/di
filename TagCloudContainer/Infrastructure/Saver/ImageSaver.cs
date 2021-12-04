using System.Drawing;
using System.Drawing.Imaging;
using TagCloudContainer.Infrastructure.Common;

namespace TagCloudContainer.Infrastructure.Saver;

public class ImageSaver : IImageSaver
{
    private readonly IOutputPathProvider settings;

    public static IReadOnlyDictionary<string, ImageFormat> ImageFormats = new Dictionary<string, ImageFormat>
    {
        ["png"] = ImageFormat.Png,
        ["bmp"] = ImageFormat.Bmp,
        ["jpg"] = ImageFormat.Jpeg,
        ["tiff"] = ImageFormat.Tiff,
        ["gif"] = ImageFormat.Gif,
        ["exif"] = ImageFormat.Exif
    };

    public ImageSaver(IAppSettings settings)
    {
        this.settings = settings;
    }

    public void Save(Bitmap bitmap)
    {
        if (!ImageFormats.TryGetValue(settings.OutputFormat.ToLowerInvariant(), out var imageFormat))
            throw new ArgumentException($"Acceptable formats: {string.Join(',', ImageFormats.Keys)}, but was {settings.OutputFormat.ToLowerInvariant()}");
            
        bitmap.Save($"{settings.OutputPath}.{settings.OutputFormat.ToLowerInvariant()}", imageFormat);
        bitmap.Dispose();
    }
}