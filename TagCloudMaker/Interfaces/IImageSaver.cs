using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud
{
    public interface IImageSaver
    {
        string SaveImage(Image image, ImageFormat format);
    }
}