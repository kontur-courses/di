using System.Drawing;
using System.Drawing.Imaging;

public class FileImageStorage : IImageStorage
{
    public void Save(Bitmap image, string path)
    {
        image.Save(path, ImageFormat.Jpeg);
    }
}