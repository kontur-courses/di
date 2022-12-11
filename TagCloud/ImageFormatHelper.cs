using System.Drawing.Imaging;

namespace TagCloud;

public static class ImageFormatHelper
{
    public static ImageFormat GetImageFormat(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return extension.ToLower() switch
        {
            ".png" => ImageFormat.Png,
            ".bmp" => ImageFormat.Bmp,
            ".ico" => ImageFormat.Icon,
            ".jpg"  => ImageFormat.Jpeg,
            ".jpeg" => ImageFormat.Jpeg,
            _ => throw new ArgumentException($"This extension {extension} are not supported!")
        };
    }
}