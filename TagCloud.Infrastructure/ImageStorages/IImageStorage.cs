using System.Drawing;

public interface IImageStorage
{
    void Save(Bitmap image, string path);
}
