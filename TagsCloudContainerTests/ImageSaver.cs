using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer;

public static class ImageSaver
{
    public static void Save(Bitmap bitmap, string path)
    {
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        bitmap.Save(path, ImageFormat.Png);
    }
}