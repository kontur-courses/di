using System.Drawing;

namespace TagsCloud2.ImageSaver;

public class ImageSaver : IImageSaver
{
    public void SaveImage(string path, string name, string format, Bitmap bitmap)
    {
        bitmap.Save(path + "\\" + name + @"." + format );
    }
}