using System.Drawing;

namespace TagsCloud2;

public interface IImageSaver
{
    public void SaveImage(string path, string name, string format, Bitmap bitmap);
}