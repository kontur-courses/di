using System.Drawing;

namespace TagsCloud2;

public class ImageSaver : IImageSaver
{
    public void SaveImage(string path, string name, string format, Bitmap bitmap)
    {
        bitmap.Save(@"D:\шпора-2022\di\TagsCloud2\img.png");
        bitmap.Save(path + @"\" + name + @"." + format );
    }
}