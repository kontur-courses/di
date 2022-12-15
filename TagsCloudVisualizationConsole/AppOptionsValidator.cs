using System.Drawing.Imaging;

namespace TagsCloudVisualizationConsole;

public static class AppOptionsValidator
{
    public static void ValidatePathsInOptions(ArgsOptions? argsOptions)
    {
        if (argsOptions == null)
            throw new ArgumentNullException(nameof(argsOptions));

        if (!File.Exists(argsOptions.PathToTextFile))
            throw new ArgumentException($"{argsOptions.PathToTextFile} does not exist");

        if (!Directory.Exists(argsOptions.DirectoryToSaveFile))
            throw new ArgumentException($"{argsOptions.DirectoryToSaveFile} does not exist");

        if (string.IsNullOrEmpty(argsOptions.SaveFileName))
            throw new ArgumentException($"Save file name empty");
    }


    public static ImageFormat GetImageFormat(string format)
    {
        return format.ToLower() switch
        {
            "png" => ImageFormat.Png,
            "bmp" => ImageFormat.Bmp,
            "emf" => ImageFormat.Emf,
            "gif" => ImageFormat.Gif,
            "icon" => ImageFormat.Icon,
            "jpeg" => ImageFormat.Jpeg,
            "tiff" => ImageFormat.Tiff,
            _ => throw new ArgumentException("ImageFormat unexpected")
        };
    }
}