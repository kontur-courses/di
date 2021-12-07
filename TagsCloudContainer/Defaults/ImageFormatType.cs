using System.Drawing.Imaging;

namespace TagsCloudContainer.Defaults;

public enum ImageFormatType
{
    Bmp,
    Jpeg,
    Png,
}

public static class ImageFormatExtensions
{
    public static ImageFormat GetFormat(this ImageFormatType formatType)
    {
        return formatType switch
        {
            ImageFormatType.Bmp => ImageFormat.Bmp,
            ImageFormatType.Jpeg => ImageFormat.Jpeg,
            ImageFormatType.Png => ImageFormat.Png,
            _ => throw new NotSupportedException(),
        };
    }
}