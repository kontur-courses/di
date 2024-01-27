using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization;

public static class BitmapExtensions
{
    public static void SaveImage(this Bitmap bitmap, string outputFilePath, ImageFormat imageFormat)
    {
        outputFilePath = Path.GetFullPath(outputFilePath);
        var outputFileName = Path.GetFileName(outputFilePath);
        var outputFileDirectory = Path.GetDirectoryName(outputFilePath);

        Directory.CreateDirectory(outputFileDirectory);

        var savePath = Path.Combine(outputFileDirectory, $"{outputFileName}.{imageFormat.ToString().ToLower()}");

        bitmap.Save(savePath, imageFormat);

        Console.WriteLine($"Image is saved to {savePath}");
    }
}