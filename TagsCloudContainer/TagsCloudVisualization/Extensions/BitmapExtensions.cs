using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.Extensions;

public static class BitmapExtensions
{
    public static void SaveAs(this Bitmap image, string directoryPath, string fileName, ImageFormat format)
    {
        if (directoryPath is null || directoryPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            throw new ArgumentException("Directory path shouldn't be null and contain forbidden symbols");
        
        if (fileName is null || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            throw new ArgumentException("File name shouldn't be null and contain forbidden symbols");
        
        Directory.CreateDirectory(directoryPath);
        image.Save(Path.Combine(directoryPath, fileName + "." + format));
    }
}