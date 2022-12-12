using System.Drawing;

namespace TagsCloud2.ImageSaver;

public interface IImageSaver
{
    public void SaveImage(string path, string name, string format, Bitmap bitmap);
}