using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Savers;

public class HardDriveSaver : IBitmapSaver
{
    public void Save(Bitmap bitmap, string filename, ImageFormat format)
    {
        filename = $"{filename}.{format}";
        bitmap.Save(filename, format);
    }
}