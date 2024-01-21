using System.Drawing;

namespace TagsCloudVisualization;

public static class Visualizer
{
    public static Bitmap Visualize(IList<Rectangle> rectangles, int bitmapWidth, int bitmapHeight)
    {
        var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
        var graphics = Graphics.FromImage(bitmap);

        foreach (var rect in rectangles)
        {
            graphics.DrawRectangle(new Pen(Color.Blue), rect);
        }

        return bitmap;
    }

    public static void SaveBitmap(Bitmap bitmap, string fileName, string pathToDirectory)
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
