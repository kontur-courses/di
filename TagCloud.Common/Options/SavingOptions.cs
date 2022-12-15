using System.Drawing.Imaging;

namespace TagCloud.Common.Options;

public class SavingOptions
{
    public string SavePath { get; }
    public string FileName { get; }
    public ImageFormat SavingImageFormat { get; }

    public SavingOptions(string savePath, string fileName, ImageFormat savingImageFormat)
    {
        SavePath = savePath;
        FileName = fileName;
        SavingImageFormat = savingImageFormat;
    }

    public static ImageFormat ConvertToImageFormat(string format)
    {
        return format switch
        {
            ".bmp" => ImageFormat.Bmp,
            ".jpeg" => ImageFormat.Jpeg,
            ".jpg" => ImageFormat.Jpeg,
            ".png" => ImageFormat.Png,
            _ => throw new BadImageFormatException("Недопустимый формат изображения")
        };
    }
}