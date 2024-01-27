using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.ImageSavers;

public class ImageSaver : IImageSaver
{
    private readonly ImageSaverSettings settings;
    
    public ImageSaver(ImageSaverSettings fileSettings) 
    {
        settings = fileSettings;
    }

    public void SaveImage(Bitmap bitmap)
    {
        if (!IsFileNameValid(settings.FileName))
        {
            throw new ArgumentException($"filename {settings.FileName} is incorrect");
        }
        if (!IsPathValid(settings.PathToSaveDirectory))
        {
            throw new ArgumentException($"path {settings.PathToSaveDirectory} is incorrect");
        }

        if (!Directory.Exists(settings.PathToSaveDirectory))
        {
            Directory.CreateDirectory(settings.PathToSaveDirectory);
        }
        bitmap.Save(Path.Combine(settings.PathToSaveDirectory, settings.FileName + "." + settings.FileFormat),
            GetImageFormat(settings.FileFormat));
    }

    public static bool IsFileNameValid(string fileName)
    {
        return !(fileName == null || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1);
    }

    public static bool IsPathValid(string filePath)
    {
        return !(filePath.IndexOfAny(Path.GetInvalidPathChars()) != -1);

    }

    public static ImageFormat GetImageFormat(string format)
    {
        try
        {
            var imageFormatConverter = new ImageFormatConverter();
            var imageFormat = imageFormatConverter.ConvertFromString(format);
            if (imageFormat != null)
                return (ImageFormat)imageFormat;
            throw new ArgumentException($"Can't convert format from this string {format}");
        }
        catch (NotSupportedException)
        {
            throw new NotSupportedException($"File with format {format} doesn't supported");
        }
    }
}
