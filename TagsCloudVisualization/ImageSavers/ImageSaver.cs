using System.Drawing;

namespace TagsCloudVisualization.ImageSavers;

public class ImageSaver : IImageSaver
{
    public void SaveImage(Bitmap bitmap, string fileName, string pathToDirectory)
    {
        if (!IsFileNameValid(fileName))
        {
            throw new ArgumentException($"filename {fileName} is incorrect");
        }
        if (!IsPathValid(pathToDirectory))
        {
            throw new ArgumentException($"path {pathToDirectory} is incorrect");
        }

        if (!Directory.Exists(pathToDirectory))
        {
            Directory.CreateDirectory(pathToDirectory);
        }

        bitmap.Save(Path.Combine(pathToDirectory, fileName), System.Drawing.Imaging.ImageFormat.Png);
    }

    public static bool IsFileNameValid(string fileName)
    {
        if (fileName == null || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            return false;
        return true;
    }

    public static bool IsPathValid(string filePath)
    {
        if (filePath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            return false;
        return true;
    }
}
