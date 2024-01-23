using System.Drawing.Imaging;

namespace TagsCloud;

public static class ImageSaver
{
    public static void SaveImage(string workingDirectory, List<Tag> tags, string imageName)
    {
 /*       var imagesDirectoryPath = Path.Combine(workingDirectory, "images");

        if (!Directory.Exists(imagesDirectoryPath)) Directory.CreateDirectory(imagesDirectoryPath);
        
        var imagePath = Path.Combine(imagesDirectoryPath, $"{imageName}.png");

        using var image = RectanglesVisualizer.RenderCloudImage(tags);
        image.Save(imagePath, ImageFormat.Png);
*/    }
}